using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public abstract class BaseMusicSceneEffect : ModSceneEffect
{
	public abstract int NPCType { get; }

	public virtual int? ProjType => null;

	public abstract int? MusicModMusic { get; }

	public abstract int VanillaMusic { get; }

	public abstract int OtherworldMusic { get; }

	public virtual int MusicDistance => 5000;

	public virtual int[] AdditionalNPCs => new int[0];

	public override int Music => SetMusic();

	public virtual bool AdditionalCheck()
	{
		return true;
	}

	public virtual int SetMusic()
	{
		if (MusicModMusic.HasValue)
		{
			return MusicModMusic.Value;
		}
		return VanillaMusic;
	}

	public virtual bool SetSceneEffect(Player player)
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		if (!AdditionalCheck())
		{
			return false;
		}
		if (!MusicModMusic.HasValue && VanillaMusic == -1)
		{
			return false;
		}
		Rectangle screenRect = default(Rectangle);
		((Rectangle)(ref screenRect))._002Ector((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
		int musicDistance = MusicDistance * 2;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		Rectangle npcBox = default(Rectangle);
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			bool inList = false;
			if (npc.type == NPCType)
			{
				inList = true;
			}
			else
			{
				for (int i = 0; i < AdditionalNPCs.Length; i++)
				{
					if (npc.type == AdditionalNPCs[i])
					{
						inList = true;
						break;
					}
				}
			}
			if (inList)
			{
				((Rectangle)(ref npcBox))._002Ector((int)npc.Center.X - MusicDistance, (int)npc.Center.Y - MusicDistance, musicDistance, musicDistance);
				if (((Rectangle)(ref screenRect)).Intersects(npcBox))
				{
					return true;
				}
			}
		}
		if (!ProjType.HasValue)
		{
			return false;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
		Rectangle projBox = default(Rectangle);
		while (enumerator2.MoveNext())
		{
			Projectile proj = enumerator2.Current;
			bool isActive = false;
			if (proj.type == ProjType)
			{
				isActive = true;
			}
			if (isActive)
			{
				((Rectangle)(ref projBox))._002Ector((int)proj.Center.X - MusicDistance, (int)proj.Center.Y - MusicDistance, musicDistance, musicDistance);
				if (((Rectangle)(ref screenRect)).Intersects(projBox))
				{
					return true;
				}
			}
		}
		return false;
	}

	public override bool IsSceneEffectActive(Player player)
	{
		return SetSceneEffect(player);
	}
}
