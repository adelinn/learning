// Initializes the `RandService` service on path `/random`
import { ServiceAddons } from '@feathersjs/feathers';
import { Application } from '../../declarations';
import { RandService } from './rand-service.class';
import createModel from '../../models/rand-service.model';
import hooks from './rand-service.hooks';

// Add this service to the service type index
declare module '../../declarations' {
  interface ServiceTypes {
    'random': RandService & ServiceAddons<any>;
  }
}

export default function (app: Application): void {
  const options = {
    Model: createModel(app),
    paginate: app.get('paginate')
  };

  // Initialize our service with any options it requires
  app.use('/random', new RandService(options, app));

  // Get our initialized service so that we can register hooks
  const service = app.service('random');

  service.hooks(hooks);
}
