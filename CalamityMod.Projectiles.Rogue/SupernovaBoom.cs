using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SupernovaBoom : ModProjectile, ILocalizedModType, IModType
{
	public int frameX;

	public int frameY;

	private const int horizontalFrames = 5;

	private const int verticalFrames = 4;

	private const int frameLength = 2;

	private const float radius = 300f;

	public bool damageFrame;

	public Color variedColor;

	public Color mainColor;

	public Color randomColor;

	public int colorTimer;

	public int time;

	public int currentFrame;

	public new string LocalizationCategory => "Projectiles.Rogue";

	private Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 408;
		base.Projectile.height = 410;
		base.Projectile.scale = 1f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		randomColor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.Red, 
			1 => Color.MediumTurquoise, 
			2 => Color.Orange, 
			_ => Color.LawnGreen, 
		});
		if (time == 0)
		{
			mainColor = randomColor;
		}
		if (time % 20 == 0)
		{
			variedColor = (Color)(colorTimer switch
			{
				0 => Color.Red, 
				1 => Color.MediumTurquoise, 
				2 => Color.Orange, 
				_ => Color.LawnGreen, 
			});
			colorTimer++;
			if (colorTimer >= 4)
			{
				colorTimer = 0;
			}
		}
		mainColor = Color.Lerp(mainColor, variedColor, 0.07f);
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref mainColor)).ToVector3() * 3f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 2 == 1)
		{
			currentFrame++;
			frameY++;
			if (frameY >= 4)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX >= 5)
			{
				base.Projectile.Kill();
			}
		}
		if (currentFrame == 13)
		{
			damageFrame = true;
		}
		else
		{
			damageFrame = false;
		}
		if (currentFrame == 4)
		{
			float numberOfDusts = 20f;
			float rotFactor = 360f / numberOfDusts;
			for (int i = 0; (float)i < numberOfDusts; i++)
			{
				randomColor = (Color)(Main.rand.Next(4) switch
				{
					0 => Color.Red, 
					1 => Color.MediumTurquoise, 
					2 => Color.Orange, 
					_ => Color.LawnGreen, 
				});
				MathHelper.ToRadians((float)i * rotFactor);
				Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f);
				velOffset *= Main.rand.NextFloat(25f, 45f);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + velOffset * 2.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f), Main.rand.NextFloat(0.1f, 0.2f), randomColor, 9));
			}
		}
		if (currentFrame == 13)
		{
			base.Projectile.velocity = Vector2.Zero;
			Owner.SetScreenshake(5f);
			int points = 5;
			float radians = (float)Math.PI * 2f / (float)points;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
			float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
			for (int k = 0; k < points; k++)
			{
				Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.45f * rotRando);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velocity * 7.5f, velocity, affectedByGravity: false, 4, 1.65f, Color.White));
			}
			for (int j = 0; j < 30; j++)
			{
				Vector2 randVel = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.8f, 1.6f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, new Color(57, 46, 115) * 0.9f, Main.rand.Next(25, 36), Main.rand.NextFloat(0.9f, 2.3f), 0.4f));
			}
			float numberOflines = 45f;
			float rotFactorlines = 360f / numberOflines;
			for (int e = 0; (float)e < numberOflines; e++)
			{
				randomColor = (Color)(Main.rand.Next(4) switch
				{
					0 => Color.Red, 
					1 => Color.MediumTurquoise, 
					2 => Color.Orange, 
					_ => Color.LawnGreen, 
				});
				float rot = MathHelper.ToRadians((float)e * rotFactorlines);
				Vector2 offset = (Vector2.UnitX * Main.rand.NextFloat(0.2f, 3.1f)).RotatedBy(rot + Main.rand.NextFloat(0.1f, 5.1f));
				Vector2 velOffset2 = (Vector2.UnitX * Main.rand.NextFloat(0.2f, 3.1f)).RotatedBy(rot + Main.rand.NextFloat(0.1f, 5.1f));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + offset, velOffset2 * Main.rand.NextFloat(15.5f, 25.5f), affectedByGravity: true, 95, Main.rand.NextFloat(0.3f, 1.1f), Color.Lerp(Color.White, randomColor, 0.3f)));
			}
		}
		if (currentFrame < 13)
		{
			for (int l = 0; l < Main.maxNPCs; l++)
			{
				NPC target = Main.npc[l];
				if (target.CanBeMoved(ignoreKBImmune: true) && target.CanBeChasedBy(base.Projectile))
				{
					if (Vector2.Distance(target.Center, base.Projectile.Center) > 40f && Vector2.Distance(target.Center, base.Projectile.Center) < 600f)
					{
						NPC nPC = target;
						nPC.Center += target.Center.DirectionTo(base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 22f;
						target.SyncMotionToServer();
					}
				}
				else
				{
					if (target == null || !CalamityPlayer.areThereAnyDamnBosses)
					{
						continue;
					}
					target = base.Projectile.Center.ClosestNPCAt(600f);
					if (target == null)
					{
						continue;
					}
					if (Vector2.Distance(target.Center, base.Projectile.Center) > 5f)
					{
						if (Vector2.Distance(target.Center, base.Projectile.Center) < 600f)
						{
							base.Projectile.velocity = base.Projectile.Center.DirectionTo(target.Center).SafeNormalize(Vector2.UnitX) * 30f;
						}
					}
					else
					{
						base.Projectile.velocity = Vector2.Zero;
						base.Projectile.Center = target.Center;
					}
				}
			}
		}
		time++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 300f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		int length = value.Width / 5;
		int height = value.Height / 4;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(frameX * length, frameY * height, length, height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)length / 2f, (float)height / 2f);
		Rectangle? sourceRectangle = frame;
		Color white = Color.White;
		((Color)(ref white)).A = 0;
		Main.EntitySpriteDraw(value, drawPos, sourceRectangle, white, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		Vector2 launchVel = base.Projectile.Center.DirectionTo(target.Center);
		target.MoveNPC(launchVel, 30f, ignoreKBImmune: true);
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 90);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.95f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override bool? CanDamage()
	{
		if (!damageFrame)
		{
			return false;
		}
		return null;
	}

	public SupernovaBoom()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		variedColor = Color.White;
		mainColor = Color.LawnGreen;
		randomColor = Color.White;
		currentFrame = 1;
		base._002Ector();
	}
}
