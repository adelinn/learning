import { Params, Paginated, Id } from '@feathersjs/feathers';
import { Service, KnexServiceOptions } from 'feathers-knex';
import { Application } from '../../declarations';
import { randomInt } from 'crypto';

function getRandArr(): Array<number>{
  const randArr: Array<number> = [];
  for(let i=0;i<5;i++) randArr.push(randomInt(0,2147483647))
  return randArr; 
}

type randRecord = {
  idNo: number | undefined,
  id: number | undefined,
  numbers: Array<number>,
  timestamp: any
}

export class RandService extends Service {
  //eslint-disable-next-line @typescript-eslint/no-unused-vars
  constructor(options: Partial<KnexServiceOptions>, app: Application) {
    super({
      ...options,
      name: 'random',
      id: 'idNo'
    });
  }

  async find(params?: Params | undefined): Promise<any[] | Paginated<any>> {
    await super.create({numbers:getRandArr()});
    return (await super.find({query:{$limit:1000}}) as Paginated<any>).data.map(x=>{x.id=x.idNo;delete x.idNo; return x;});
  }
}
