import { Asset } from "./Asset";
import { IModel } from "./IModel"

export class Invoice implements IModel {
    public id: number | undefined;
    public date: Date | undefined;
    public year: number | undefined;
    public month: number | undefined;
    public total: number | undefined;
    public assets: Array<Asset> | undefined
}