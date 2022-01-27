import { inject, injectable } from "inversify";
import { makeAutoObservable } from "mobx";
import ownTypes from "../ioc/ownTypes";
import type { Note } from "../models/Note";
import type NoteService from "../services/NoteService";
import i18n from "../locales/config"

@injectable()
export default class NoteStore {

    note : Note | null = null;
    isLoading = false;
    error = '';
    updateError = '';
    queryString = '';
    isDeleted = false;
    result = '';
    title = '';
    description = '';
    author = '';
    isUpdated = false;

    constructor(   
        @inject(ownTypes.noteService) private readonly noteService: NoteService
   ) {
       makeAutoObservable(this);
   }

    
    public search = async () => {
      this.isDeleted = false;
        this.error = '';
        try {
            this.isLoading = true;
            const id = Number(this.queryString);
            if (id === NaN) {
                this.queryString = '';
                this.error = i18n.t('user:error.input');
                return;
            }
            const result = await this.noteService.getById(id);
            this.note = { ...result };
            
          } catch (e) {
            if (e instanceof Error) {
                this.queryString = '';
                this.error = e.message;
                this.note = null;
            }
          }
        this.isLoading = false;
    }

    
    public changeQueryString = (query: string) : void => {
      this.queryString = query;
    }

    public delete = async () => {
        try {
          this.isLoading = true;
          const id = Number(this.queryString);
          if (id === NaN) {
              this.queryString = '';
              this.error = i18n.t('user:error.input');
              return;
          }
          const result = await this.noteService.delete(id);
          if (result !== null) {
            this.isDeleted = true;
            this.result = result.result;
            this.note = null;
          }
          
        } catch (e) {
          if (e instanceof Error) {
              this.queryString = '';
              this.error = e.message;
          }
        }
      this.isLoading = false;
    }

    public update = async () => {
      this.updateError = '';
      try {
        const id = Number(this.queryString);
            if (id === NaN) {
              this.queryString = '';
              this.updateError = i18n.t('user:error.input');
              return;
          }
          if (!this.CheckValidation()){
              return;
          }
          else {
              this.isLoading = true;
              const result = await this.noteService.update(id, this.title, this.description, this.author);
              if (result !== null) {
                this.isUpdated = true;
                this.result = result.result;
                this.search();
              }
          }
        } catch (e) {
          if (e instanceof Error) {
              this.updateError = e.message;
              this.note = null;
          }
        }
        this.isLoading = false;
  }

    public changeTitle = (title: string) => {
      this.title = title;
    }

    public changeDescription = (description: string) => {
        this.description = description;
    }

    public changeAuthor = (author: string) => {
        this.author = author;
    }

    private CheckValidation() : boolean {
        if (this.author.length === 0) {
            this.error = "Author must be entered!";
        }
        if (this.title.length === 0) {
            this.error = "Title must be entered!";
        }
        if (this.description.length === 0) {
          this.error = "Description must be entered!";
      }
        return this.error.length === 0;
    }
}
