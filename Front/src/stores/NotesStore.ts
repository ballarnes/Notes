import { inject, injectable } from "inversify";
import { makeAutoObservable } from "mobx";
import ownTypes from "../ioc/ownTypes";
import { Note } from "../models/Note";
import NoteService from "../services/NoteService";

@injectable()
export default class NotesStore {

    notes : Note[] = [];
    isLoading = false;
    totalPages = 0;
    currentPage = 1;
    pageSize = 3;

    constructor(   
        @inject(ownTypes.noteService) private readonly noteService: NoteService
   ) {
       makeAutoObservable(this);
   }

    
    public init = async () => {
        this.getByPage(this.pageSize, this.currentPage);
    }

    
    public changePage = async (page: number) => {
        this.currentPage = page;
        this.getByPage(this.pageSize, page);
    }

    private getByPage = async (pageSize: number, pageIndex: number) => {
        try {
            this.isLoading = true;
            const result = await this.noteService.getByPage(pageSize, pageIndex-1);
            this.notes = result.data;
            this.totalPages = result.pagesCount;
          } catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
          }
        this.isLoading = false;
    }
}
