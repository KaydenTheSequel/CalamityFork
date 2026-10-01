using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneWave : ModProjectile, ILocalizedModType, IModType
{
	private int x;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(x);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		x = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 60 : 114);
		dust.noGravity = true;
		dust.velocity = base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.7f);
		dust.scale = Main.rand.NextFloat(0.9f, 1.8f);
		x++;
		base.Projectile.velocity.Y = (float)(5.0 * Math.Sin((double)x / 5.0));
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 12)
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
			base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 1140) / 30f, 0f, 1f);
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f * base.Projectile.Opacity, 0f, 0f);
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = 1;
		}
		else
		{
			base.Projectile.spriteDirection = -1;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int framing = texture.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		if (CalamityGlobalNPC.SCal != -1 && Main.npc[CalamityGlobalNPC.SCal].active && Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().permafrost)
		{
			((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).R = 0;
		}
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)framing / 2f), base.Projectile.scale, spriteEffects, 0f);
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
			target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 120);
		}
	}
}
