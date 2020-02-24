using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Odh.BooksDemo.Entities
{
    public enum Genre
    {
        [Description("Science Fiction")]
        ScienceFiction = 1,
        Satire = 2,
        Drama = 3,
        [Description("Action and Adventure")]
        ActionAndAdventure = 4,
        Romance = 5,
        Mystery = 6,
        Horror = 7,
        [Description("Self Help")]
        SelfHelp = 8

    }
}
