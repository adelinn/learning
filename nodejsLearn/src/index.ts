import express from '@feathersjs/express';
const app = express();
const server = app.listen(3030);

process.on('unhandledRejection', (reason, p) =>
  console.error('Unhandled Rejection at: Promise ', p, reason)
);

server.on('listening', () =>
  console.info('Feathers application started on http://%s:%d', "host.example.com", 3030)
);