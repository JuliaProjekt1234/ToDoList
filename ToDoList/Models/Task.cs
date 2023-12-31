﻿namespace ToDoList.Models;

public class Task: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }

    public int TableId { get; set; }
    public virtual Table Table { get; set; } = null!;

    public Task()
    {

    }

}
