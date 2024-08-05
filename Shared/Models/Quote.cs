using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Models
{
	public class Quote
	{
        [Key]
		[JsonPropertyName("id")]
		//Primary key attribute needs to have a setter
		public int Id { get; set; }
		[JsonPropertyName("message")]
		public string Message { get; set; }
		[JsonPropertyName("personName")]
		public string? PersonName { get; set; }
    }
}
