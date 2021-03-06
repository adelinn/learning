let ENV: string; //Deployment environment; Docker/GCP/Azure/Other
if(process.env.APPLICATIONINSIGHTS_CONNECTION_STRING) ENV = 'Azure';
else if(process.env.PORT && process.env.INSTANCE_CONNECTION_NAME) ENV = 'GCP';
else ENV = 'Other';

export default ENV;