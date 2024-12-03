using System;
using System.Collections.Generic;

namespace DatabaseFirstWithWebAPI.Models;

public partial class ToDo
{
    public int Id { get; set; }

    public string Task { get; set; } = null!;

    public bool IsCompleted { get; set; }
}
