import "reflect-metadata";
import { inject, injectable } from "inversify";
import { ContentType, MethodType } from "./HttpService";
import type { HttpService } from "./HttpService";
import ownTypes from "../ioc/ownTypes";
import type { NotesDto } from "../dtos/NotesDto";
import { NoteDto } from "../dtos/NoteDto";
import { NoteResponse } from "../dtos/NoteResponse";
import { CreateResponse } from "../dtos/CreateResponse";
import { EditResponse } from "../dtos/EditResponse";

export interface NoteService {
    getById(id: number): Promise<NoteDto>;
    getByPage(pagesize: number, page: number): Promise<NotesDto>;
}

@injectable()
export default class DefaultNoteService implements NoteService {
    public constructor(
        @inject(ownTypes.httpService) private readonly httpService: HttpService
    ) {
    }

    public async getById(id: number): Promise<NoteDto> {
        const headers = { contentType: ContentType.Json};
        const data = { id };
        const result = await this.httpService.send<NoteResponse>(`Note/GetById/`, MethodType.POST, headers, data);
        return result.data;
    }

    public async getByPage(pageSize: number, pageIndex: number): Promise<NotesDto> {
        const headers = { contentType: ContentType.Json};
        const data = { pageSize, pageIndex };
        const result = await this.httpService.send<NotesDto>(`Note/GetNotes/`, MethodType.POST, headers, data);
        return result.data;
    }

    public async delete(id: number): Promise<EditResponse> {
        const headers = { contentType: ContentType.Json};
        const data = { id };
        const result = await this.httpService.send<EditResponse>(`Note/RemoveNote/`, MethodType.POST, headers, data);
        return result.data;
    }

    public async create(title: string, description: string, author: string): Promise<CreateResponse> {
        const headers = { contentType: ContentType.Json};
        const data = { title, description, author };
        const result = await this.httpService.send<CreateResponse>(`Note/AddNote/`, MethodType.POST, headers, data);
        return result.data;
    }

    public async update(id: number, title: string, description: string, author: string): Promise<EditResponse> {
        const headers = { contentType: ContentType.Json};
        const data = { id, title, description, author };
        const result = await this.httpService.send<EditResponse>(`Note/UpdateNote/`, MethodType.POST, headers, data);
        return result.data;
    }
}

