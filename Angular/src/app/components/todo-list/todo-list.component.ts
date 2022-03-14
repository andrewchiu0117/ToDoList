import { Component, OnInit } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';
import { TodoService } from 'src/app/services/todo.service';
import { Todo } from 'src/app/interfaces/todo';
// import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss'],
  providers: [TodoService],
  animations: [
    trigger('fade', [

      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate(200, style({ opacity: 1, transform: 'translateY(0px)'}))
      ]),

      transition(':leave', [
        animate(200, style({ opacity: 0, transform: 'translateY(30px)' }))
      ]),

    ])
  ]
})
export class TodoListComponent implements OnInit {
  todoTitle: string;
  content:boolean
  constructor(private todoService: TodoService) {
  }

  ngOnInit() {
  
    this.todoTitle = '';
  }
  showContent(){
    this.content=!this.content
  }
  addTodo(): void {
    if (this.todoTitle.trim().length === 0) {
      return;
    }

    this.todoService.addTodo(this.todoTitle);

    this.todoTitle = '';
  }
  sort(command:string){
    this.todoService.sort(command)
    this.content=!this.content
  }
}

