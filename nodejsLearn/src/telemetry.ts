import ENV from './iEnv';

let trackStartup: Function = function(){};

if(ENV == 'Azure') {
    const appInsights = require('applicationinsights');
    appInsights.setup(process.env.APPLICATIONINSIGHTS_CONNECTION_STRING);
    appInsights.defaultClient.context.tags[appInsights.defaultClient.context.keys.cloudRole] = "NodeJS-API";
    appInsights.start();
    var start = Date.now();
    trackStartup = function(){
        let duration = Date.now() - start;
        appInsights.defaultClient.trackMetric({name: "server startup time", value: duration});
    }
} else if(ENV == 'GCP'){
    const { diag, DiagConsoleLogger, DiagLogLevel } = require("@opentelemetry/api");
    const { registerInstrumentations } = require('@opentelemetry/instrumentation');
    const { NodeTracerProvider } = require("@opentelemetry/sdk-trace-node");
    const { SimpleSpanProcessor } = require("@opentelemetry/sdk-trace-base");
    const {
    TraceExporter,
    } = require("@google-cloud/opentelemetry-cloud-trace-exporter");

    const { HttpInstrumentation } = require('@opentelemetry/instrumentation-http');
    const { ExpressInstrumentation } = require('@opentelemetry/instrumentation-express');
    const { FeathersInstrumentation } = require('./instrumentation-feathers');

    // Enable OpenTelemetry exporters to export traces to Google Cloud Trace.
    // Exporters use Application Default Credentials (ADCs) to authenticate.
    // See https://developers.google.com/identity/protocols/application-default-credentials
    // for more details.
    const provider = new NodeTracerProvider();

    diag.setLogger(new DiagConsoleLogger(), DiagLogLevel.ERROR);
    provider.register();

    registerInstrumentations({
        instrumentations: [
          new HttpInstrumentation(),
          new ExpressInstrumentation(),
          new FeathersInstrumentation()
        ],
        tracerProvider: provider,
    });

    // Initialize the exporter. When your application is running on Google Cloud,
    // you don't need to provide auth credentials or a project id.
    const exporter = new TraceExporter();

    // Configure the span processor to send spans to the exporter
    provider.addSpanProcessor(new SimpleSpanProcessor(exporter));
}

export { trackStartup };