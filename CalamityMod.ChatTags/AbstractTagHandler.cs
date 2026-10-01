using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalamityMod.ChatTags;

public abstract class AbstractTagHandler<TSelf> : ITagHandler, ILoadable where TSelf : AbstractTagHandler<TSelf>, new()
{
	protected abstract string[] TagNames { get; }

	public abstract TextSnippet Parse(string text, Color baseColor = default(Color), string options = null);

	public virtual void Load(Mod mod)
	{
		ChatManager.Register<TSelf>(TagNames);
	}

	public virtual void Unload()
	{
	}
}
