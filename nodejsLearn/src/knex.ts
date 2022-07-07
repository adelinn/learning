import knex from 'knex';
import { Application } from './declarations';

export default function (app: Application): void {
  const { client, connection } = app.get('postgres');
  const dbSocketPath = process.env.DB_SOCKET_PATH || '/cloudsql';
  let conn;
  if(process.env.DB_USER) 
    conn = {
      user: process.env.DB_USER, // e.g. 'my-user'
      password: process.env.DB_PASS, // e.g. 'my-user-password'
      database: process.env.DB_NAME, // e.g. 'my-database'
      host: dbSocketPath+'/'+process.env.INSTANCE_CONNECTION_NAME, // e.g. '/cloudsql/project:region:instance'
    };
  else conn = connection;
  const db = knex({ client, connection: conn });

  app.set('knexClient', db);
}
