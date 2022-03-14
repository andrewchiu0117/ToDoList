import { Injectable } from '@angular/core';
import { Todo } from '../interfaces/todo';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError as observableThrowError } from 'rxjs';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  todoTitle: string = '';
  idForTodo: number = 4;
  beforeEditCache: string = '';
  filter: string = 'all';
  anyRemainingModel: boolean = true;
  todos: Todo[] = [];

  constructor(private http: HttpClient) {
    this.todos = this.getTodos();
  }

  getTodos(): Todo[] {
    this.http.get(API_URL + 'lists')
      .pipe(catchError(this.errorHandler))
      .subscribe((response: any) => {
        this.todos = response;
        console.log(this.todos);
      })

      return this.todos;
  }

  errorHandler(error: HttpErrorResponse) {
    return observableThrowError(error.message || 'Something went wrong!!!!');
  }

  addTodo(todoTitle: string): void {
    if (todoTitle.trim().length === 0) {
      return;
    }

    this.http.post(API_URL, {
      title: todoTitle,
      completed: false
    })
      .subscribe((response: any) => {
        this.todos.push({
          id: response.id,
          title: todoTitle,
          priority: 0,
          completed: false,
          editing: false
        });
      });
      this.todos = this.getTodos();

    this.idForTodo++;
  }

  editTodo(todo: Todo): void {
    this.beforeEditCache = todo.title;
    todo.editing = true;
  }

  doneEdit(todo: Todo): void {
    if (todo.title.trim().length === 0) {
      todo.title = this.beforeEditCache;
    }

    this.anyRemainingModel = this.anyRemaining();
    todo.editing = false;

    this.http.put(API_URL, {
      id: todo.id,
      title: todo.title,
      completed: todo.completed,
      priority: todo.priority
    })
      .subscribe((response: any) => {

      })
  }

  priorityEdit(todo: Todo):void{
    this.http.put(API_URL, {
      id: todo.id,
      title: todo.title,
      completed: todo.completed,
      priority: todo.priority
    })
      .subscribe((response: any) => {
        
      })
  }

  cancelEdit(todo: Todo): void {
    todo.title = this.beforeEditCache;
    todo.editing = false;
  }

  deleteTodo(id: number): void {

    this.http.delete(API_URL + id)
    .subscribe((response: any) => {
        this.todos = this.todos.filter(todo => todo.id !== id);
      })
  }

  remaining(): number {
    return this.todos.filter(todo => !todo.completed).length;
  }

  atLeastOneCompleted(): boolean {
    return this.todos.filter(todo => todo.completed).length > 0;
  }

  clearCompleted(): void {
    const completed = this.todos
      .filter(todo => todo.completed)
      .map(todo => todo.id)

    this.http.delete(API_URL + 'deleteCompleted')
    .subscribe((response: any) => {
      this.todos = this.todos.filter(todo => !todo.completed);
    })
  }

  checkAllTodos(): void {
    const checkedTodo = (<HTMLInputElement>event.target).checked;

    this.http.put(API_URL + 'checkAll', {
      completed: checkedTodo
    })
      .subscribe((response: any) => {
        this.todos.forEach(todo => todo.completed = checkedTodo)
        this.anyRemainingModel = this.anyRemaining();
      })
  }

  anyRemaining(): boolean {
    return this.remaining() !== 0;
  }

  todosFiltered(): Todo[] {
    if (this.filter === 'all') {
      return this.todos;
    } else if (this.filter === 'active') {
      return this.todos.filter(todo => !todo.completed);
    } else if (this.filter === 'completed') {
      return this.todos.filter(todo => todo.completed);
    }

    return this.todos;
  }

  sort(command:string){
    if(command=="priority")
      this.todos= this.todos.sort((a,b)=> b.priority-a.priority)
    if(command=="content")
      this.todos= this.todos.sort((a,b)=> a.title.toUpperCase().charCodeAt(0)-b.title.toUpperCase().charCodeAt(0))
  }
}
