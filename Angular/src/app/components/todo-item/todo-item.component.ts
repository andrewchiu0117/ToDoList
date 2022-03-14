import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';


import { Todo } from 'src/app/interfaces/todo';
import { TodoService } from 'src/app/services/todo.service';


@Component({
  selector: 'todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.scss'],
  animations: []
})
export class TodoItemComponent implements OnInit {
  @Input() todo: Todo;
  public isShowpriorityOptional:boolean =false
  constructor(private todoService: TodoService) { }

  ngOnInit() {
  }
  showDialog(){
    this.isShowpriorityOptional = !this.isShowpriorityOptional
  }
 
  

  remove(id:number){
    this.todoService.deleteTodo(id)

  }
}