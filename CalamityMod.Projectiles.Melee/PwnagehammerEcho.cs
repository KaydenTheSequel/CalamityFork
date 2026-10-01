using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PwnagehammerEcho : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle BigSound = new SoundStyle("CalamityMod/Sounds/Item/PwnagehammerBigImpact")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle Kunk = new SoundStyle("CalamityMod/Sounds/Item/TF2PanHit")
	{
		Volume = 1.1f
	};

	public int Explodamage;

	public float speed;

	public NPC targeted;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.aiStyle = 0;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Color value = default(Color);
		((Color)(ref value))._002Ector(255, 248, 124, 255);
		((Color)(ref value)).A = 0;
		return value;
	}

	public override void AI()
	{
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		speed = ((Vector2)(ref base.Projectile.velocity)).Length();
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] < 42f)
		{
			base.Projectile.velocity.Y *= 0.9575f;
			base.Projectile.velocity.X *= 0.98f;
			base.Projectile.rotation += MathHelper.ToRadians(base.Projectile.ai[0] * 0.5f) * base.Projectile.localAI[0] * (float)base.Projectile.direction;
		}
		else if (base.Projectile.ai[0] >= 42f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
			base.Projectile.extraUpdates = 2;
			if (base.Projectile.ai[1] != -5f)
			{
				targeted = Main.npc[(int)base.Projectile.ai[1]];
			}
			if (targeted == null || !targeted.CanBeChasedBy(base.Projectile) || !targeted.active)
			{
				base.Projectile.ai[1] = -5f;
				targeted = base.Projectile.Center.ClosestNPCAt(2000f);
			}
			if (targeted != null)
			{
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.6f, 25f, 0.98f);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		if (Main.rand.NextBool())
		{
			Vector2 offset = Utils.RotatedByRandom(new Vector2(7f, 0f), MathHelper.ToRadians(360f));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(3f, 0f), (double)offset.ToRotation(), default(Vector2));
			Dust.NewDustPerfect(base.Projectile.Center + offset, 228, (Vector2?)new Vector2(base.Projectile.velocity.X * 0.2f + velOffset.X, base.Projectile.velocity.Y * 0.2f + velOffset.Y), 100, new Color(255, 245, 198), 2f).noGravity = true;
		}
		if (Main.rand.NextBool(6))
		{
			Vector2 offset2 = Utils.RotatedByRandom(new Vector2(7f, 0f), MathHelper.ToRadians(360f));
			Vector2 velOffset2 = Utils.RotatedBy(new Vector2(3f, 0f), (double)offset2.ToRotation(), default(Vector2));
			Dust.NewDustPerfect(base.Projectile.Center + offset2, 228, (Vector2?)new Vector2(base.Projectile.velocity.X * 0.2f + velOffset2.X, base.Projectile.velocity.Y * 0.2f + velOffset2.Y), 100, new Color(255, 245, 198), 2f).noGravity = true;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (targeted != null && target != targeted)
		{
			modifiers.SourceDamage *= 0.3f;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[0] <= 42f)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target == targeted)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreKill(int timeLeft)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		float numberOfDusts = 45f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(15f, 0f), (double)rot, default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(12.5f, 0f), (double)rot, default(Vector2));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, 269, velOffset);
			dust.noGravity = true;
			dust.velocity = velOffset * ((i % 2 == 0) ? 0.9f : ((i % 3 == 0) ? 0.8f : 1f));
			dust.scale = 3f;
		}
		if (Main.zenithWorld)
		{
			SoundEngine.PlaySound(in Kunk, base.Projectile.Center);
		}
		else
		{
			SoundEngine.PlaySound(in BigSound, base.Projectile.Center);
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<PwnagehammerExplosionBig>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color gold = Color.Gold;
		((Color)(ref gold)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, gold * 0.5f, 1, texture, drawCentered: true, shrink: true);
		return false;
	}
}
