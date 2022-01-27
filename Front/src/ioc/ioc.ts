import { Container } from 'inversify';
import type { AuthenticationService} from '../services/AuthenticationService';
import DefaultAuthenticationService from '../services/AuthenticationService';
import type { HttpService} from '../services/HttpService';
import DefaultHttpService from '../services/HttpService';
import type { NoteService} from '../services/NoteService';
import DefaultNoteService from '../services/NoteService';
import HomePageStore from '../stores/HomePageStore'
import NotesStore from '../stores/NotesStore'
import LoginStore from '../stores/LoginStore'
import ownTypes from './ownTypes';
import NoteStore from '../stores/NoteStore';
import CreateNoteStore from '../stores/CreateNoteStore';

export const container = new Container();
container.bind<HttpService>(ownTypes.httpService).to(DefaultHttpService).inSingletonScope();
container.bind<NoteService>(ownTypes.noteService).to(DefaultNoteService).inSingletonScope();
container.bind<AuthenticationService>(ownTypes.authenticationService).to(DefaultAuthenticationService).inSingletonScope();
container.bind<HomePageStore>(ownTypes.homePageStore).to(HomePageStore).inTransientScope();
container.bind<NoteStore>(ownTypes.noteStore).to(NoteStore).inTransientScope();
container.bind<CreateNoteStore>(ownTypes.createNoteStore).to(CreateNoteStore).inTransientScope();
container.bind<NotesStore>(ownTypes.notesStore).to(NotesStore).inTransientScope();
container.bind<LoginStore>(ownTypes.loginStore).to(LoginStore).inTransientScope(); 
