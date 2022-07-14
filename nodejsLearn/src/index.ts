if(process.env.APPLICATIONINSIGHTS_CONNECTION_STRING) {
  const appInsights = require('applicationinsights');
  appInsights.setup(process.env.APPLICATIONINSIGHTS_CONNECTION_STRING).start();
}
import logger from './logger';
import app from './app';


const port = process.env.PORT || process.env.CONTAINER_APP_PORT || app.get('port');
const server = app.listen(port);

process.on('unhandledRejection', (reason, p) =>
  logger.error('Unhandled Rejection at: Promise ', p, reason)
);

server.on('listening', () =>
  logger.info('Feathers application started on http://%s:%d', app.get('host'), port)
);
