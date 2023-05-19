import { Asset } from "./Asset";

export class Invoice {
    public id: number | undefined;
    public date: Date | undefined;
    public year: number | undefined;
    public month: number | undefined;
    public total: number | undefined;
    public assets: Array<Asset> | undefined
}