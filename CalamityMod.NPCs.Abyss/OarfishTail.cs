using System;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Abyss;

[LongDistanceNetSync(SyncWith = typeof(OarfishHead))]
public class OarfishTail : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.OarfishHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 15;
		base.NPC.width = 14;
		base.NPC.height = 16;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 4000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		base.NPC.chaseable = false;
		base.Banner = ModContent.NPCType<OarfishHead>();
		base.BannerItem = ModContent.ItemType<OarfishBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		bool shouldDespawn = true;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.type == ModContent.NPCType<OarfishHead>())
			{
				shouldDespawn = false;
				break;
			}
		}
		if (!shouldDespawn)
		{
			if (base.NPC.ai[1] <= 0f)
			{
				shouldDespawn = true;
			}
			else if (Main.npc[(int)base.NPC.ai[1]].life <= 0)
			{
				shouldDespawn = true;
			}
		}
		if (shouldDespawn)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha < 128)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		Vector2 segmentPosition = default(Vector2);
		((Vector2)(ref segmentPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
		float targetXDirection = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2);
		float targetYDirection = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2);
		targetXDirection = (int)(targetXDirection / 16f) * 16;
		targetYDirection = (int)(targetYDirection / 16f) * 16;
		segmentPosition.X = (int)(segmentPosition.X / 16f) * 16;
		segmentPosition.Y = (int)(segmentPosition.Y / 16f) * 16;
		targetXDirection -= segmentPosition.X;
		targetYDirection -= segmentPosition.Y;
		float targetDistance = (float)Math.Sqrt(targetXDirection * targetXDirection + targetYDirection * targetYDirection);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentPosition = new Vector2(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				targetXDirection = Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) - segmentPosition.X;
				targetYDirection = Main.npc[(int)base.NPC.ai[1]].position.Y + (float)(Main.npc[(int)base.NPC.ai[1]].height / 2) - segmentPosition.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(targetYDirection, targetXDirection) + 1.57f;
			targetDistance = (float)Math.Sqrt(targetXDirection * targetXDirection + targetYDirection * targetYDirection);
			int segmentWidth = base.NPC.width;
			targetDistance = (targetDistance - (float)segmentWidth) / targetDistance;
			targetXDirection *= targetDistance;
			targetYDirection *= targetDistance;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X = base.NPC.position.X + targetXDirection;
			base.NPC.position.Y = base.NPC.position.Y + targetYDirection;
			if (targetXDirection < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (targetXDirection > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("OarfishTail").Type);
			}
		}
	}
}
