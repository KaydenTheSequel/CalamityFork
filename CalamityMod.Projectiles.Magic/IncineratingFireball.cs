using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class IncineratingFireball : ModProjectile, ILocalizedModType, IModType
{
	public bool Released;

	public bool TriggeredBurnOut;

	public const float StartScale = 0.0004f;

	public const float EndScale = 10.25f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60000;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		Vector2 center = base.Projectile.Center;
		Color red = Color.Red;
		Lighting.AddLight(center, ((Color)(ref red)).ToVector3() * (base.Projectile.scale * 0.5f));
		if (TriggeredBurnOut)
		{
			Owner.Calamity().burningSeaBurnOut = 150;
		}
		bool canUseMana = Owner.CheckMana(Owner.HeldItem);
		if (Owner.CantUseHoldout() || !canUseMana || base.Projectile.timeLeft <= 40)
		{
			Released = true;
			if ((float)base.Projectile.timeLeft > 40f)
			{
				base.Projectile.timeLeft = (int)MathHelper.Clamp(MathHelper.Lerp(0f, 40f, Timer / 240f), 0f, 40f);
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.965f;
			base.Projectile.scale = Utils.Remap(base.Projectile.timeLeft, 0f, 40f, 0.0004f, 10.25f);
			base.Projectile.ExpandHitboxBy((int)(base.Projectile.scale * 50f));
		}
		else if (!Released)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 projLocation = Owner.Center;
				Vector2 val = Owner.ClampedMouseWorld();
				float mouseDist = Vector2.Distance(val, projLocation);
				Vector2 mouseDiff = val - projLocation;
				if (mouseDist > 128f)
				{
					((Vector2)(ref mouseDiff)).Normalize();
					mouseDiff *= 128f;
				}
				projLocation += mouseDiff;
				Vector2 orbAttemptedVelocity = Vector2.Zero.MoveTowards(projLocation - base.Projectile.Center, 25f);
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, orbAttemptedVelocity, 0.08f);
				base.Projectile.netUpdate = true;
			}
			base.Projectile.scale = Utils.Remap(Timer, 0f, 240f, 0.0004f, 10.25f);
			base.Projectile.ExpandHitboxBy((int)(base.Projectile.scale * 50f));
			if (Timer % 15f == 0f)
			{
				Owner.CheckMana(Owner.HeldItem, -1, pay: true);
			}
			if (Timer > 240f && Timer < 560f)
			{
				for (int s = 0; s < 6; s++)
				{
					float sparkRotation = Main.GlobalTimeWrappedHourly * -5.75f + (float)Math.PI / 3f * (float)s;
					Vector2 val2 = base.Projectile.Center + Vector2.UnitX.RotatedBy(sparkRotation) * 220f;
					Vector2 sparkVelocity = Vector2.Normalize(val2 - base.Projectile.Center).RotatedBy(MathHelper.ToRadians(70f)) * 2f;
					GeneralParticleHandler.SpawnParticle(new AltLineParticle(val2, sparkVelocity, affectedByGravity: false, 8, 0.8f, Color.Lerp(Color.Red, Color.Orange, Main.rand.NextFloat(0.3f))));
				}
			}
			if (Timer > 470f)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(220f, 220f);
					Vector2 smokeVel = -Vector2.UnitY * Main.rand.NextFloat(7f, 13f);
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(position, smokeVel, new Color(192, 192, 192), 10, 0.7f, 0.6f));
				}
			}
			if (Timer > 560f)
			{
				base.Projectile.timeLeft = 40;
				SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/WeaponEnchant"), Owner.Center);
				CombatText.NewText(Owner.Hitbox, new Color(192, 0, 0), CalamityUtils.GetTextValue("Misc.BurningSeaBurn"), dramatic: true);
				Owner.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
				TriggeredBurnOut = true;
			}
		}
		AdjustPlayerValues();
	}

	public void AdjustPlayerValues()
	{
		base.Projectile.spriteDirection = (base.Projectile.direction = Owner.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
	}

	public override bool? CanDamage()
	{
		return !Released;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/WeldingBurn");
		soundStyle.Volume = 0.25f;
		SoundStyle Burn = soundStyle;
		SoundEngine.PlaySound(in Burn, target.Center);
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		for (int i = 0; i < 4; i++)
		{
			Vector2 smokeVel = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(2.5f, 5f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(target.Center, smokeVel, Color.Gray, 36, 0.7f, 0.625f, 0f, glowing: true));
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D TheodoreJNoise = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoise", (AssetRequestMode)2).Value;
		Vector2 drawScale = base.Projectile.Size / TheodoreJNoise.Size() * 1.25f;
		float rotation = Main.GlobalTimeWrappedHourly * 4.2f;
		GameShaders.Misc["CalamityMod:ExoVortex"].UseOpacity(0.6f);
		GameShaders.Misc["CalamityMod:ExoVortex"].Apply();
		for (int i = 0; i < 6; i++)
		{
			float direction = (i % 2 == 0).ToDirectionInt();
			float offsetDist = 0f;
			Color fireballColor = Color.Lerp(new Color(255, 200, 200), new Color(255, 30, 30), MathHelper.Clamp(Timer / 240f, 0f, 1f));
			if (Timer > 440f)
			{
				fireballColor = Color.Lerp(new Color(255, 30, 30), Color.Red, MathHelper.Clamp((Timer - 560f + 120f) / 60f, 0f, 1f));
				offsetDist = MathHelper.Lerp(5f, 40f, (Timer - 560f + 120f) / 120f);
			}
			Main.spriteBatch.Draw(TheodoreJNoise, base.Projectile.Center - Main.screenPosition + Main.rand.NextVector2Circular(offsetDist, offsetDist), (Rectangle?)null, fireballColor, direction * rotation, TheodoreJNoise.Size() / 2f, drawScale, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
