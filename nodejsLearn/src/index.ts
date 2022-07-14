let start: number;
let trackStartup: Function;
//import appInsights from 'applicationinsights';
if(process.env.APPLICATIONINSIGHTS_CONNECTION_STRING) {
  const appInsights = require('applicationinsights');
  appInsights.setup(process.env.APPLICATIONINSIGHTS_CONNECTION_STRING);
  appInsights.defaultClient.context.tags[appInsights.defaultClient.context.keys.cloudRole] = "NodeJS-API";
  appInsights.start();
  start = Date.now();
  trackStartup = function(){
    let duration = Date.now() - start;
    appInsights.defaultClient.trackMetric({name: "server startup time", value: duration});
  }
}
import logger from './logger';
import app from './app';


const port = process.env.PORT || process.env.CONTAINER_APP_PORT || app.get('port');
const server = app.listen(port);

process.on('unhandledRejection', (reason, p) =>
  logger.error('Unhandled Rejection at: Promise ', p, reason)
);

server.on('listening', () => {
  if(start) {
    trackStartup();
  }
  return logger.info('Feathers application started on http://%s:%d', app.get('host'), port);
});
