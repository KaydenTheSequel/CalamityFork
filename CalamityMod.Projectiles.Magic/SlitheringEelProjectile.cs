using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SlitheringEelProjectile : ModProjectile, ILocalizedModType, IModType
{
	public struct EelSegment
	{
		public Vector2 CurrentPosition;

		public float Rotation;

		public EelSegment(Vector2 position, float rotation)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			CurrentPosition = position;
			Rotation = rotation;
		}
	}

	public EelSegment[] Segments = new EelSegment[10];

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 360;
		base.Projectile.Opacity = 0f;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if ((float)base.Projectile.numHits >= 3f)
		{
			base.Projectile.alpha += 5;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.1f, 0f, 1f);
		}
		Vector2 mouse = Main.player[base.Projectile.owner].ClampedMouseWorld();
		if ((float)base.Projectile.timeLeft % 80f < 35f && base.Projectile.Distance(mouse) > 70f)
		{
			float angleDifference = MathHelper.WrapAngle(base.Projectile.AngleTo(mouse) - base.Projectile.velocity.ToRotation());
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(angleDifference / 9f);
		}
		if ((float)base.Projectile.timeLeft % 65f == 64f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.UnitY * 7f, ModContent.ProjectileType<EelDrop>(), base.Projectile.damage / 2, 2f, base.Projectile.owner);
		}
		UpdateSegments();
	}

	public void UpdateSegments()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aheadPosition = base.Projectile.Center;
		float aheadRotation = base.Projectile.rotation;
		for (int i = 0; i < Segments.Length; i++)
		{
			Vector2 offsetToDestination = aheadPosition - Segments[i].CurrentPosition;
			if (aheadRotation != base.Projectile.rotation)
			{
				float offsetAngle = MathHelper.WrapAngle(aheadRotation - base.Projectile.rotation) * 0.03f;
				offsetToDestination = offsetToDestination.RotatedBy(offsetAngle);
			}
			Segments[i].Rotation = (aheadPosition - Segments[i].CurrentPosition).ToRotation();
			Segments[i].CurrentPosition = aheadPosition - offsetToDestination.SafeNormalize(Vector2.UnitY) * 13f;
			aheadPosition = Segments[i].CurrentPosition;
			aheadRotation = Segments[i].Rotation;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D headTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/SlitheringEelProjectile", (AssetRequestMode)2).Value;
		Texture2D bodyTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/SlitheringEelBody", (AssetRequestMode)2).Value;
		Texture2D bodyTexture2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/SlitheringEelBody2", (AssetRequestMode)2).Value;
		Texture2D tailTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/SlitheringEelTail", (AssetRequestMode)2).Value;
		lightColor = new Color(255, 255, 255, 127);
		Vector2 drawPosition;
		for (int i = 0; i < Segments.Length; i++)
		{
			Texture2D textureToUse = ((i % 2 == 1) ? bodyTexture2 : bodyTexture);
			if (i == Segments.Length - 1)
			{
				textureToUse = tailTexture;
			}
			drawPosition = Segments[i].CurrentPosition - Main.screenPosition;
			Main.EntitySpriteDraw(textureToUse, drawPosition, null, base.Projectile.GetAlpha(lightColor), Segments[i].Rotation, textureToUse.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(headTexture, drawPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, headTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Length; i++)
		{
			if (((Rectangle)(ref targetHitbox)).Intersects(Utils.CenteredRectangle(Segments[i].CurrentPosition, Vector2.One * 12f)))
			{
				return true;
			}
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 23; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 75);
		}
	}
}
