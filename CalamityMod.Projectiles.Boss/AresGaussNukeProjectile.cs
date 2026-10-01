using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.Skies;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AresGaussNukeProjectile : ModProjectile, ILocalizedModType, IModType
{
	private const int timeLeft = 180;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.CooldownSlot = 1;
		base.Projectile.timeLeft = 180;
		if (Main.zenithWorld)
		{
			base.Projectile.extraUpdates = 1;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.position.Y > base.Projectile.ai[1])
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 12)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) - (float)Math.PI / 2f;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			for (int i = 0; i < 25; i++)
			{
				float num = Main.rand.NextFloat(3f, 13f);
				float angleRandom = 0.06f;
				Vector2 dustVel = Utils.RotatedBy(new Vector2(num, 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				dustVel = dustVel.RotatedBy(0f - angleRandom);
				dustVel = dustVel.RotatedByRandom(2f * angleRandom);
				float scale = Main.rand.NextFloat(0.5f, 1.6f);
				Dust.NewDustPerfect(base.Projectile.Center, 107, -dustVel, 0, default(Color), scale).noGravity = true;
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				int totalProjectiles = 12;
				float radians = (float)Math.PI * 2f / (float)totalProjectiles;
				int type = ModContent.ProjectileType<AresGaussNukeProjectileSpark>();
				float velocity = ((Vector2)(ref base.Projectile.velocity)).Length();
				double angleA = (double)radians * 0.5;
				double angleB = (double)MathHelper.ToRadians(90f) - angleA;
				float velocityX2 = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
				Vector2 spinningPoint = (Main.rand.NextBool() ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX2, 0f - velocity));
				for (int k = 0; k < totalProjectiles; k++)
				{
					Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2 + Vector2.Normalize(base.Projectile.velocity) * -6f, type, AresGaussNuke.SparkDamage, 0f, Main.myPlayer);
				}
			}
		}
		Lighting.AddLight(base.Projectile.Center, 0.2f, 0.25f, 0.05f);
		int target = Player.FindClosest(base.Projectile.Center, 1, 1);
		Vector2 distanceFromTarget = Main.player[target].Center - base.Projectile.Center;
		if (CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 45f, Main.player[target].Hitbox))
		{
			base.Projectile.Kill();
			return;
		}
		float stopHomingDistance = (death ? 280f : (revenge ? 290f : (expertMode ? 300f : 320f)));
		if ((((Vector2)(ref distanceFromTarget)).Length() < stopHomingDistance && base.Projectile.ai[0] != -1f) || base.Projectile.ai[0] == 1f)
		{
			base.Projectile.ai[0] = 1f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 24f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.025f;
			}
		}
		else
		{
			float scaleFactor = ((Vector2)(ref base.Projectile.velocity)).Length();
			float inertia = (death ? 8f : (revenge ? 9f : (expertMode ? 10f : 12f)));
			((Vector2)(ref distanceFromTarget)).Normalize();
			distanceFromTarget *= scaleFactor;
			base.Projectile.velocity = (base.Projectile.velocity * inertia + distanceFromTarget) / (inertia + 1f);
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= scaleFactor;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		Texture2D telegraphBase = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseOpacity(0.2f * MathHelper.Clamp((1f - (float)base.Projectile.timeLeft / 180f) * 8f, 0f, 1f));
		GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseColor(Color.Lerp(Color.Goldenrod, Color.Gold, 0.7f * (float)Math.Pow(0.5 + 0.5 * Math.Sin(Main.GlobalTimeWrappedHourly), 3.0)));
		GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseSecondaryColor(Color.Lerp(Color.Yellow, Color.White, 0.5f));
		GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseSaturation(1f - (float)base.Projectile.timeLeft / 180f);
		GameShaders.Misc["CalamityMod:CircularAoETelegraph"].Apply();
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, lightColor, 0f, telegraphBase.Size() / 2f, 1480f, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/AresGaussNukeProjectileGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in AresGaussNuke.NukeExplosionSound, base.Projectile.Center);
		if (!Main.dedServ)
		{
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, base.Projectile.velocity, base.Mod.Find<ModGore>("AresGaussNuke1").Type);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, base.Projectile.velocity, base.Mod.Find<ModGore>("AresGaussNuke3").Type);
		}
		ExoMechsSky.CreateLightningBolt(12);
		if (Main.myPlayer != base.Projectile.owner || base.Projectile.ai[0] == -1f)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			Projectile explosion = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AresGaussNukeProjectileBoom>(), (i == 2) ? base.Projectile.damage : 0, 0f, Main.myPlayer);
			if (explosion.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				explosion.ai[1] = 560f + (float)i * 90f;
				explosion.localAI[1] = 0.25f;
				explosion.Opacity = MathHelper.Lerp(0.18f, 0.6f, (float)i / 7f) + Main.rand.NextFloat(-0.08f, 0.08f);
				explosion.netUpdate = true;
			}
		}
	}
}
