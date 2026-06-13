using p5rpc.rpc.Template.Configuration;
using System.ComponentModel;

namespace p5rpc.rpc.Configuration
{
    public class Config : Configurable<Config>
    {
        /*
            User Properties:
                - Please put all of your configurable properties here.

            By default, configuration saves as "Config.json" in mod user config folder.    
            Need more config files/classes? See Configuration.cs

            Available Attributes:
            - Category
            - DisplayName
            - Description
            - DefaultValue

            // Technically Supported but not Useful
            - Browsable
            - Localizable

            The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
        */

        [DisplayName("Discord Application ID")]
        [Description("The Discord Application (client) ID used for rich presence. Leave as default to use the original mod's images, or set your own app's ID after uploading custom Art Assets (see the repo's LOCATIONS_GUIDE.md).")]
        [DefaultValue("1032265834111975424")]
        public string DiscordAppId { get; set; } = "1032265834111975424";

        [DisplayName("Debug Mode")]
        [Description("Logs additional information to the console that is useful for debugging.")]
        public bool DebugEnabled { get; set; } = false;

    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // 
    }
}