using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Models;

public partial class ReplyTable
{
    [JsonIgnore]
    public int? IdPublication { get; set; }

    public int IdReply { get; set; }

    public string? Content { get; set; }

    [JsonIgnore]
    public virtual PublicationTable? IdPublicationNavigation { get; set; }
}
