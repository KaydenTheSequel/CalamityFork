using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class AstralachneaGround : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/AstralachneaGroundGlow", (AssetRequestMode)2);
		}
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.width = 70;
		base.NPC.height = 34;
		base.NPC.aiStyle = 3;
		base.NPC.damage = 55;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 500;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.knockBackResist = 0.38f;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.NPC.timeLeft = NPC.activeTime * 2;
		base.AnimationType = 164;
		base.Banner = ModContent.NPCType<AstralachneaWall>();
		base.BannerItem = ModContent.ItemType<AstralachneaBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 90;
			base.NPC.defense = 40;
			base.NPC.knockBackResist = 0.28f;
			base.NPC.lifeMax = 750;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest();
		if (Main.netMode == 1 || base.NPC.velocity.Y != 0f)
		{
			return;
		}
		int x = (int)base.NPC.Center.X / 16;
		int y = (int)base.NPC.Center.Y / 16;
		bool transform = false;
		for (int i = x - 1; i <= x + 1; i++)
		{
			for (int j = y - 1; j <= y + 1; j++)
			{
				if (Main.tile[i, j] != null && Main.tile[i, j].WallType > 0)
				{
					transform = true;
				}
			}
		}
		if (transform)
		{
			base.NPC.Transform(ModContent.NPCType<AstralachneaWall>());
		}
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		int frame = base.NPC.frame.Y / frameHeight;
		Rectangle rect = default(Rectangle);
		((Rectangle)(ref rect))._002Ector(62, 4, 14, 6);
		switch (frame)
		{
		case 1:
			((Rectangle)(ref rect))._002Ector(64, 6, 12, 6);
			break;
		case 2:
			((Rectangle)(ref rect))._002Ector(58, 8, 22, 6);
			break;
		case 3:
			((Rectangle)(ref rect))._002Ector(54, 8, 26, 8);
			break;
		case 4:
			((Rectangle)(ref rect))._002Ector(58, 6, 20, 8);
			break;
		}
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 80, frameHeight, ModContent.DustType<AstralOrange>(), rect, Vector2.Zero, 0.45f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 4, 22);
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			for (int i = 0; i < 6; i++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.3f, base.Mod.Find<ModGore>("AstralachneaGore" + i).Type);
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(40f, 21f);
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos, (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, origin, 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(2))
		{
			return 0.17f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		AstralachneaWall.ModifyAstralachneaLoot(npcLoot);
	}
}
