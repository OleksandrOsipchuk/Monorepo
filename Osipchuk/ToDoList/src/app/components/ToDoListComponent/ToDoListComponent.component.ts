import {Component} from '@angular/core';
import {Task} from "./Task";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'to-do-list',
  templateUrl: './ToDoListComponent.component.html',
  styleUrls: ['./ToDoListComponent.component.scss']
})
export class TaskComponent  {
  taskList:Task[]= JSON.parse(localStorage.getItem('tasks') || '[]')
  save(form:NgForm){
    let task =form.controls['newTask'].value
    if(task!==null && task!==''){
      let newTask= new Task()
      newTask.info=form.controls['newTask'].value
      newTask.isComplete=false
      this.taskList.push(newTask);
      localStorage.setItem('tasks',JSON.stringify(this.taskList))
      form.reset('')
    }
    else alert('empty')
  }
  delete(index:number){
    this.taskList.splice(index,1)
    localStorage.setItem('tasks',JSON.stringify(this.taskList))
  }
  makeDone(task:Task){
    task.isComplete = !task.isComplete
  }
}
