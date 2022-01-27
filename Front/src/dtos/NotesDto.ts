import { NoteDto } from "./NoteDto";

export interface NotesDto {
    data: NoteDto[],
    pagesCount: number
  }