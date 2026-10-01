using CalamityMod.Items.Weapons.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PrismaticMagicCircle : ModProjectile, ILocalizedModType, IModType
{
	public int Lifetime = 360;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Timer => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 512);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = MeleeRangedHybridDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = Lifetime;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		if (Owner.CantUseHoldout() && base.Projectile.timeLeft > 30)
		{
			base.Projectile.timeLeft = 30;
		}
		if (Owner.active && !Owner.dead)
		{
			base.Projectile.Center = Owner.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 60f;
		}
		Vector2 aimVector = (Main.MouseWorld - Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true)).SafeNormalize(Vector2.UnitY);
		aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(base.Projectile.velocity), 0.94f));
		if (aimVector != base.Projectile.velocity)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = aimVector;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Timer < 30f)
		{
			base.Projectile.scale = MathHelper.Lerp(0f, 1f, Timer / 30f);
		}
		else
		{
			base.Projectile.scale = Utils.GetLerpValue(0f, 30f, base.Projectile.timeLeft, clamped: true);
		}
		if (Timer == 1f && Main.myPlayer == base.Projectile.owner)
		{
			SoundEngine.PlaySound(in SoundID.Item67, base.Projectile.Center);
			SoundEngine.PlaySound(in SoundID.Item68, base.Projectile.Center);
			SoundStyle style = TeslaCannon.FireSound with
			{
				Pitch = 1f
			};
			SoundEngine.PlaySound(in style);
			Vector2 spawnPos = Vector2.Lerp(base.Projectile.Center, Owner.Center, 0.5f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, base.Projectile.velocity, ModContent.ProjectileType<PrismaticRay>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D howNoisy = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoise", (AssetRequestMode)2).Value;
		Vector2 squishScale = new Vector2((float)(base.Projectile.width / howNoisy.Width) * 0.55f, (float)(base.Projectile.height / howNoisy.Height) * 2f) * base.Projectile.scale * 0.36f;
		GameShaders.Misc["CalamityMod:ExoVortex"].Apply();
		for (int i = 0; i < 6; i++)
		{
			Main.spriteBatch.Draw(howNoisy, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.White, base.Projectile.rotation, howNoisy.Size() / 2f, squishScale, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
