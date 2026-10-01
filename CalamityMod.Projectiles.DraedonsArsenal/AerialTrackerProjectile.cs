using System;
using System.Linq;
using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AerialTrackerProjectile : ModProjectile, ILocalizedModType, IModType
{
	public NPC extraShotTarget;

	public const int LaserFireRate = 19;

	public const int LaserFireRateStealth = 25;

	public const int MaxLaserCountPerShot = 2;

	public const float MaxTargetSearchDistance = 600f;

	public const float MaxTargetSearchStealth = 800f;

	public const float ReturnAccelerationFactor = 0.0012f;

	public const float ReturnMaxSpeed = 6f;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Items/Weapons/DraedonsArsenal/AerialTracker";

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
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 52;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color val = Color.Lerp(ArsenalEffects.ArsenalLaserColor, Color.White, 0.5f);
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3() * 0.5f);
		Player player = Main.player[base.Projectile.owner];
		Time++;
		if (Time == 5f && !base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.tileCollide = true;
		}
		if (Time < 100f && Time > 10f && base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(-0.029f * base.Projectile.ai[2]);
		}
		if (!ReturningToPlayer)
		{
			if (Time >= 55f)
			{
				ReturningToPlayer = true;
				base.Projectile.tileCollide = false;
				base.Projectile.netUpdate = true;
			}
		}
		else
		{
			float distanceFromPlayer = base.Projectile.Distance(player.Center);
			if (distanceFromPlayer > 3000f)
			{
				base.Projectile.Kill();
			}
			Vector2 idealVelocity = (player.Center - base.Projectile.Center) / distanceFromPlayer * 6f * (base.Projectile.Calamity().stealthStrike ? (Utils.GetLerpValue(90f, 300f, Time, clamped: true) * 1.5f) : Utils.GetLerpValue(60f, 300f, Time, clamped: true));
			base.Projectile.velocity.X += (float)Math.Sign(idealVelocity.X - base.Projectile.velocity.X) * (0.0012f * Time);
			base.Projectile.velocity.Y += (float)Math.Sign(idealVelocity.Y - base.Projectile.velocity.Y) * (0.0012f * Time);
			if (Time % (float)(base.Projectile.Calamity().stealthStrike ? 25 : 19) == 0f)
			{
				AttemptToFireLasers(base.Projectile.damage);
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
		if (ReturningToPlayer)
		{
			base.Projectile.rotation += 0.28f * Utils.GetLerpValue(30f, 300f, Time, clamped: true);
		}
		else
		{
			base.Projectile.rotation += 0.15f;
		}
	}

	public void AttemptToFireLasers(int damage)
	{
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			int targetCount = 0;
			foreach (NPC target in Main.npc.Where(delegate(NPC npc)
			{
				//IL_000f: Unknown result type (might be due to invalid IL or missing references)
				return npc.active && base.Projectile.Distance(npc.Center) < 800f && npc.CanBeChasedBy();
			}).ToList())
			{
				if (targetCount >= 2)
				{
					break;
				}
				_ = base.Projectile.Center + Utils.RotatedBy(new Vector2(25f, 0f), (double)(base.Projectile.rotation * Utils.GetLerpValue(300f, 30f, Time, clamped: true)), default(Vector2));
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AerialTrackerLaser>(), damage, base.Projectile.knockBack, base.Projectile.owner, 1f, base.Projectile.whoAmI, target.whoAmI);
				projectile.scale *= 1.4f;
				projectile.netUpdate = true;
				targetCount++;
				extraShotTarget = target;
			}
			if (targetCount == 1)
			{
				_ = base.Projectile.Center + Utils.RotatedBy(new Vector2(25f, 0f), (double)((0f - base.Projectile.rotation) * Utils.GetLerpValue(300f, 30f, Time, clamped: true)), default(Vector2));
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AerialTrackerLaser>(), damage, base.Projectile.knockBack, base.Projectile.owner, -1f, base.Projectile.whoAmI, extraShotTarget.whoAmI);
			}
		}
		else
		{
			NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(600f);
			if (potentialTarget != null)
			{
				_ = base.Projectile.Center + Utils.RotatedBy(new Vector2(25f, 0f), (double)(base.Projectile.rotation * 0.4f), default(Vector2));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AerialTrackerLaser>(), damage, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.whoAmI, potentialTarget.whoAmI);
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> obj = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = obj.Size() * 0.5f;
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(obj.Value, drawPosition, null, lightColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
