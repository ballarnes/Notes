import { injectable } from "inversify";
import { makeAutoObservable } from "mobx";

export enum TabsType {
  Note,
  Notes,
  CreateNote
}

@injectable()
export default class HomePageStore {

    currentTab = TabsType[TabsType.Note];

    constructor(   
   ) {
       makeAutoObservable(this);
   }
    
    public changeTab = (tab: string | null) : void => {
      this.currentTab = !!tab ? tab : TabsType[TabsType.Note];
    }
}
