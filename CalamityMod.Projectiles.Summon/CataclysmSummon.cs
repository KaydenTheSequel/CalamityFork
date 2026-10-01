using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CataclysmSummon : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Time => ref base.Projectile.ai[0];

	public bool LookingAtPlayer => Time < 45f;

	public override string Texture => "CalamityMod/NPCs/SupremeCalamitas/SupremeCataclysm";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.MinionSacrificable[base.Type] = false;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = false;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 100);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 0f;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		if (LookingAtPlayer)
		{
			base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(0f, 9f, Time / 45f));
		}
		else
		{
			float punchInterpolant = (Time - 45f) / 35f % 1f;
			base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(12f, 21f, punchInterpolant));
		}
		Behavior(base.Projectile, Main.player[base.Projectile.owner], ref Time);
	}

	public static void Behavior(Projectile projectile, Player owner, ref float time)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		if (time < 40f)
		{
			FadeIn(projectile);
		}
		else if (time > 95f)
		{
			projectile.Opacity -= 0.06f;
			if (projectile.Opacity < 0f)
			{
				projectile.Kill();
			}
		}
		if (time < 40f)
		{
			projectile.velocity = projectile.SafeDirectionTo(owner.Center) * -5f;
			projectile.rotation = projectile.velocity.X * 0.01f;
			projectile.spriteDirection = (owner.Center.X < projectile.Center.X).ToDirectionInt();
		}
		else if (time == 40f)
		{
			projectile.velocity = projectile.SafeDirectionTo(owner.Center) * 31f;
			projectile.rotation = projectile.velocity.X * 0.0125f;
			projectile.damage = (int)((double)projectile.damage * 1.45);
			SoundEngine.PlaySound(in SoundID.DD2_BetsyFlameBreath, owner.Center);
			SoundEngine.PlaySound(in SoundID.DD2_WyvernDiveDown, owner.Center);
		}
		time++;
	}

	public static void FadeIn(Projectile projectile)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust dust = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Square(-65f, 65f), 267);
				dust.velocity = -Vector2.UnitY * Main.rand.NextFloat(1.8f, 3.6f);
				dust.scale = Main.rand.NextFloat(1.65f, 1.85f);
				dust.fadeIn = Main.rand.NextFloat(0.7f, 0.9f);
				dust.color = Color.Red;
				dust.noGravity = true;
			}
			projectile.localAI[0] = 1f;
		}
		projectile.Opacity = MathHelper.Clamp(projectile.Opacity + 0.075f, 0f, 1f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(3, 9, base.Projectile.frame / 9, base.Projectile.frame % 9);
		Vector2 origin = frame.Size() * 0.5f;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float afterimageRot = base.Projectile.oldRot[i];
			SpriteEffects sfxForThisAfterimage = (SpriteEffects)(base.Projectile.oldSpriteDirection[i] == -1);
			Vector2 drawPos = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
			Color color = base.Projectile.GetAlpha(lightColor) * ((float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length);
			Main.EntitySpriteDraw(texture, drawPos, frame, color, afterimageRot, origin, base.Projectile.scale, sfxForThisAfterimage);
		}
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= 0f;
		if (Main.masterMode)
		{
			modifiers.SourceDamage.Flat += 320f;
		}
		else if (Main.expertMode)
		{
			modifiers.SourceDamage.Flat += 260f;
		}
		else
		{
			modifiers.SourceDamage.Flat += 200f;
		}
	}

	public override bool? CanDamage()
	{
		return base.Projectile.Opacity >= 1f;
	}
}
