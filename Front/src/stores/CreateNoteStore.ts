import { makeAutoObservable } from "mobx";
import { inject, injectable } from 'inversify';
import ownTypes from "../ioc/ownTypes";
import NoteService from "../services/NoteService";

@injectable()
export default class CreateNoteStore {
    public id = 0;
    public title = '';
    public description = '';
    public author = '';
    public error = '';
    public isLoading = false;

    public constructor(
        @inject(ownTypes.noteService) private readonly noteService: NoteService
    ) {
        makeAutoObservable(this);
    }

    public create = async () => {
        this.id = 0;
        this.error = '';
        try {
            if (!this.CheckValidation()){
                return;
            }
            else {
                this.isLoading = true;
                const result = await this.noteService.create(this.title, this.description, this.author);
                this.id = result.id;
            }
          } catch (e) {
            if (e instanceof Error) {
                this.error = e.message;
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
        return this.error.length === 0;
    }
}