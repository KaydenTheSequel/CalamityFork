using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneHellblast2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1500;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 2f && (Main.expertMode || BossRushEvent.BossRushActive) && base.Projectile.timeLeft < 1260 && ((Vector2)(ref base.Projectile.velocity)).Length() < 10f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.002f;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 10)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft < 60)
		{
			base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / 60f, 0f, 1f);
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 1440) / 60f, 0f, 1f);
		}
		Lighting.AddLight(base.Projectile.Center, 0.9f * base.Projectile.Opacity, 0f, 0f);
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (float)Math.Atan2(0f - base.Projectile.velocity.Y, 0f - base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int drawStart = frameHeight * base.Projectile.frame;
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		if (CalamityGlobalNPC.SCal != -1 && NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()) && Main.npc[CalamityGlobalNPC.SCal].active && Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().permafrost)
		{
			((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).R = 0;
		}
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, drawStart, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.Opacity == 1f)
		{
			if (base.Projectile.ai[0] == 0f || Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 180);
			}
			else
			{
				target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
			}
		}
	}
}
