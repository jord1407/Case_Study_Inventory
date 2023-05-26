import { IModel } from "./IModel";

export class Asset implements IModel {
    public id: number | undefined;
    public name: string | undefined;
    public price: number | undefined;
    public validFrom: Date | undefined;
    public validTo: Date | undefined;
}