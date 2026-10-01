using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DynamicPursuerProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const float MaxTargetSearchDistance = 800f;

	public float ElectricVelocityCharge;

	public float LaserVelocityCharge;

	public bool Ricochet;

	public NPC nextTarget;

	public int glowmaskFrame;

	public float ReturnAcceleration = DynamicPursuer.ReturnAcceleration;

	public float ReturnMaxSpeed = DynamicPursuer.ReturnMaxSpeed;

	public float RicochetVelocityCap = DynamicPursuer.RicochetVelocityCap;

	public float ElectricityDmgMult = DynamicPursuer.ElectricityDmgMult;

	public float ElectricityCooldown = DynamicPursuer.ElectricityCooldown;

	public float RicochetShootingCooldown = DynamicPursuer.RicochetShootingCooldown;

	public float LaserDmgMult = DynamicPursuer.LaserDmgMult;

	public float LaserCooldown = DynamicPursuer.LaserCooldown;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public bool ReturningToPlayer
	{
		get
		{
			return base.Projectile.ai[0] == 1f;
		}
		set
		{
			base.Projectile.ai[0] = value.ToInt();
		}
	}

	public float Time
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 320;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color blue = Color.Blue;
		Lighting.AddLight(center, ((Color)(ref blue)).ToVector3());
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			glowmaskFrame++;
			base.Projectile.frameCounter = 0;
		}
		if (glowmaskFrame >= 9)
		{
			glowmaskFrame = 0;
		}
		Time++;
		if (!ReturningToPlayer)
		{
			if (Time >= 45f && !Ricochet)
			{
				ReturningToPlayer = true;
				base.Projectile.tileCollide = false;
				base.Projectile.netUpdate = true;
			}
			else if (Ricochet)
			{
				if (nextTarget != null)
				{
					base.Projectile.velocity = (float)Math.Pow(Math.E, Time / 175f) * (nextTarget.Center - base.Projectile.Center).SafeNormalize(Vector2.One);
					if (base.Projectile.velocity.X > RicochetVelocityCap)
					{
						base.Projectile.velocity.X = RicochetVelocityCap;
					}
					if (base.Projectile.velocity.X < 0f - RicochetVelocityCap)
					{
						base.Projectile.velocity.X = 0f - RicochetVelocityCap;
					}
					if (base.Projectile.velocity.Y > RicochetVelocityCap)
					{
						base.Projectile.velocity.Y = RicochetVelocityCap;
					}
					if (base.Projectile.velocity.Y < 0f - RicochetVelocityCap)
					{
						base.Projectile.velocity.Y = 0f - RicochetVelocityCap;
					}
				}
				else
				{
					Ricochet = false;
					ReturningToPlayer = true;
					base.Projectile.tileCollide = false;
					base.Projectile.netUpdate = true;
				}
				ElectricVelocityCharge += ((Vector2)(ref base.Projectile.velocity)).Length();
				if (ElectricVelocityCharge >= RicochetShootingCooldown)
				{
					ElectricVelocityCharge = 0f;
					AttemptToFireElectricity((int)((float)base.Projectile.damage * ElectricityDmgMult));
					AttemptToFireLasers((int)((float)base.Projectile.damage * LaserDmgMult));
				}
			}
		}
		else
		{
			float distanceFromPlayer = base.Projectile.Distance(player.Center);
			if (distanceFromPlayer > 2800f)
			{
				base.Projectile.Kill();
			}
			if (base.Projectile.Calamity().stealthStrike)
			{
				ReturnMaxSpeed = (float)Math.Pow(Math.E, Time / 125f);
			}
			else
			{
				ReturnMaxSpeed = (float)Math.Pow(Math.E, Time / 150f);
			}
			Vector2 idealVelocity = (player.Center - base.Projectile.Center) / distanceFromPlayer * ReturnMaxSpeed;
			ReturnAcceleration = (float)Math.Pow(Math.E, Time / 300f);
			base.Projectile.velocity.X += (float)Math.Sign(idealVelocity.X - base.Projectile.velocity.X) * ReturnAcceleration;
			base.Projectile.velocity.Y += (float)Math.Sign(idealVelocity.Y - base.Projectile.velocity.Y) * ReturnAcceleration;
			if (base.Projectile.velocity.X > RicochetVelocityCap)
			{
				base.Projectile.velocity.X = RicochetVelocityCap;
			}
			if (base.Projectile.velocity.X < 0f - RicochetVelocityCap)
			{
				base.Projectile.velocity.X = 0f - RicochetVelocityCap;
			}
			if (base.Projectile.velocity.Y > RicochetVelocityCap)
			{
				base.Projectile.velocity.Y = RicochetVelocityCap;
			}
			if (base.Projectile.velocity.Y < 0f - RicochetVelocityCap)
			{
				base.Projectile.velocity.Y = 0f - RicochetVelocityCap;
			}
			ElectricVelocityCharge += ((Vector2)(ref base.Projectile.velocity)).Length();
			LaserVelocityCharge += ((Vector2)(ref base.Projectile.velocity)).Length();
			if (ElectricVelocityCharge >= ElectricityCooldown)
			{
				ElectricVelocityCharge = 0f;
				AttemptToFireElectricity((int)((float)base.Projectile.damage * ElectricityDmgMult));
			}
			if (base.Projectile.Calamity().stealthStrike && LaserVelocityCharge >= LaserCooldown)
			{
				LaserVelocityCharge = 0f;
				base.Projectile.velocity = Vector2.Zero;
				AttemptToFireLasers((int)((float)base.Projectile.damage * LaserDmgMult));
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
				{
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.rotation += 0.25f;
	}

	public void AttemptToFireElectricity(int damage)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(800f);
		Vector2 blueGem = (Vector2.UnitY * -12f).RotatedBy(base.Projectile.rotation);
		if (potentialTarget != null)
		{
			Vector2 initialVelocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * 2f;
			if (Main.rand.NextBool())
			{
				initialVelocity = initialVelocity.RotatedByRandom(0.4000000059604645);
			}
			float initialAngle = initialVelocity.ToRotation();
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + blueGem, base.Projectile.SafeDirectionTo(potentialTarget.Center) * 3f, ModContent.ProjectileType<DynamicPursuerElectricity>(), damage, base.Projectile.knockBack, base.Projectile.owner, initialAngle, Main.rand.Next(100));
		}
	}

	public void AttemptToFireLasers(int damage)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		Vector2 direction1 = default(Vector2);
		((Vector2)(ref direction1))._002Ector((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-10, 10));
		Vector2 direction2 = default(Vector2);
		((Vector2)(ref direction2))._002Ector((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-10, 10));
		Vector2 redExtremity1 = Utils.RotatedBy(new Vector2(-27f, 7f), (double)base.Projectile.rotation, default(Vector2));
		Vector2 redExtremity2 = Utils.RotatedBy(new Vector2(27f, 7f), (double)base.Projectile.rotation, default(Vector2));
		SoundStyle style = SoundID.Item12 with
		{
			Volume = SoundID.Item12.Volume * 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + redExtremity1, direction1 * 3f, ModContent.ProjectileType<DynamicPursuerLaser>(), damage, base.Projectile.knockBack, base.Projectile.owner);
		style = SoundID.Item12 with
		{
			Volume = SoundID.Item12.Volume * 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + redExtremity2, direction2 * 3f, ModContent.ProjectileType<DynamicPursuerLaser>(), damage, base.Projectile.knockBack, base.Projectile.owner);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in CommonCalamitySounds.SwiftSliceSound, base.Projectile.position);
		if ((base.Projectile.Calamity().stealthStrike && base.Projectile.numHits == 4) || (!base.Projectile.Calamity().stealthStrike && !ReturningToPlayer))
		{
			if (base.Projectile.Calamity().stealthStrike && base.Projectile.numHits == 4 && !ReturningToPlayer)
			{
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PlasmaGrenadeSmallExplosion>(), base.Projectile.damage * 3 / 4, base.Projectile.knockBack * 2f, base.Projectile.owner);
				}
				for (int i = 0; i < 220; i++)
				{
					int type = (Main.rand.NextBool() ? 261 : 107);
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), type);
					dust.scale = Main.rand.NextFloat(1.6f, 2.2f);
					dust.velocity = Main.rand.NextVector2CircularEdge(75f, 75f);
					dust.noGravity = true;
					if (type == 261)
					{
						dust.velocity *= 1.5f;
					}
				}
			}
			ReturningToPlayer = true;
			Ricochet = false;
			return;
		}
		Ricochet = true;
		NPC newTarget = null;
		float closestNPCDistance = 2800f;
		float targettingDistance = 1600f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		Vector2 val;
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI == target.whoAmI || !n.CanBeChasedBy(base.Projectile))
			{
				continue;
			}
			val = base.Projectile.Center - n.Center;
			float potentialNewDistance = ((Vector2)(ref val)).Length();
			if (potentialNewDistance < targettingDistance && potentialNewDistance < closestNPCDistance)
			{
				closestNPCDistance = potentialNewDistance;
				newTarget = (nextTarget = n);
				if (base.Projectile.timeLeft < 300)
				{
					base.Projectile.timeLeft = 300;
				}
			}
		}
		if (newTarget == null)
		{
			val = base.Projectile.Center - Main.npc[target.whoAmI].Center;
			float potentialNewDistance2 = ((Vector2)(ref val)).Length();
			if (potentialNewDistance2 < targettingDistance && potentialNewDistance2 < closestNPCDistance)
			{
				closestNPCDistance = potentialNewDistance2;
				newTarget = Main.npc[target.whoAmI];
				nextTarget = newTarget;
				base.Projectile.timeLeft += 300;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		ReturningToPlayer = true;
		base.Projectile.tileCollide = false;
		base.Projectile.netUpdate = true;
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, ProjectileID.Sets.TrailCacheLength[base.Type]);
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowmaskTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/DynamicPursuerProjectileGlowmask", (AssetRequestMode)2).Value;
		Rectangle glowmaskRectangle = glowmaskTexture.Frame(1, 9, 0, glowmaskFrame);
		Vector2 origin = glowmaskRectangle.Size() / 2f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)0;
		Main.EntitySpriteDraw(value, drawPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(glowmaskTexture, drawPosition, glowmaskRectangle, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}
}
