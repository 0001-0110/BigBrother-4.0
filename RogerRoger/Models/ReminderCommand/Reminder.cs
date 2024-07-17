using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogerRoger.Models.ReminderCommand
{
    [Table("Reminders")]
    public class Reminder : IModel<int>
    {
        [Key]
        public int Id { get; internal set; }

        public DateTime DueDate { get; }
        public string Text { get; }

        internal Reminder() { }

        public Reminder(DateTime dueDate, string text)
        {
            DueDate = dueDate;
            Text = text;
        }
    }
}
