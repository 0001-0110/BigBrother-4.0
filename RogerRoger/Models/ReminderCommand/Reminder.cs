using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogerRoger.Models.ReminderCommand
{
    [Table("reminders")]
    public class Reminder : IModel<int>
    {
        [Key]
        public int Id { get; internal set; }

        public ulong UserId { get; internal set; }
        public DateTime DueDate { get; internal set; }
        public string Text { get; internal set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        internal Reminder() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Reminder(ulong userId, DateTime dueDate, string text)
        {
            UserId = userId;
            DueDate = dueDate;
            Text = text;
        }
    }
}
