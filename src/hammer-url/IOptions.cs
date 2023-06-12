using PeanutButter.EasyArgs.Attributes;

namespace hammer_url
{
    public interface IOptions
    {
        [ShortName('n')]
        [Default(1)]
        [Min(1)]
        int Concurrency { get; set; }

        [Required]
        string Url { get; set; }

        [Description("Set this to not output a period for every completed request")]
        bool Quiet { get; set; }
    }
}