using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Models;

public partial class PublicationTable
{

    public int IdPublication { get; set; }

    public string? Content { get; set; }

    [JsonIgnore]
    public virtual ICollection<ReplyTable> ReplyTables { get; set; } = new List<ReplyTable>();
}
