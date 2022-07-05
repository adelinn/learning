import assert from 'assert';
import app from '../../src/app';

describe('\'RandService\' service', () => {
  it('registered the service', () => {
    const service = app.service('random');

    assert.ok(service, 'Registered the service');
  });
});
