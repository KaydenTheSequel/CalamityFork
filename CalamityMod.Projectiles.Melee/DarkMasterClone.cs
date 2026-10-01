using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DarkMasterClone : ModProjectile, ILocalizedModType, IModType
{
	public Player clone;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void AI()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.HeldItem.type != ModContent.ItemType<TheDarkMaster>() || !Owner.active || Owner.CCed || Owner == null)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.velocity = Vector2.Zero;
		Vector2 moveTo = Vector2.UnitY * -160f;
		float num = base.Projectile.ai[0];
		if (num != 1f)
		{
			if (num == 2f)
			{
				((Vector2)(ref moveTo))._002Ector(180f, 120f);
			}
		}
		else
		{
			((Vector2)(ref moveTo))._002Ector(-180f, 120f);
		}
		base.Projectile.timeLeft = 2;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Owner.Center + moveTo, 0.4f);
		if (base.Projectile.Distance(Owner.Center + moveTo) < 16f)
		{
			base.Projectile.ai[2] = 1f;
		}
		if (base.Projectile.ai[2] == 0f)
		{
			Vector2 angleVec = Main.rand.NextFloat(0f, (float)Math.PI * 2f).ToRotationVector2();
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, angleVec * Main.rand.NextFloat(1f, 2f), Color.Black, 30, Main.rand.NextFloat(0.25f, 1f), 0.5f, 0.1f));
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] = 0f;
			Vector2 direction = base.Projectile.Center.DirectionTo(Main.MouseWorld);
			base.Projectile.direction = Math.Sign(direction.X);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, direction * Owner.HeldItem.shootSpeed, ModContent.ProjectileType<DarkMasterBeam>(), (int)((float)base.Projectile.damage * 0.4f), base.Projectile.knockBack, base.Projectile.owner, 1f, 1f);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath6, base.Projectile.Center);
		for (int i = 0; i < 8; i++)
		{
			Vector2 angleVec = Main.rand.NextFloat(0f, (float)Math.PI * 2f).ToRotationVector2();
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, angleVec * Main.rand.NextFloat(2f, 4f), Color.Black, 60, Main.rand.NextFloat(0.45f, 1.22f), 0.6f, 0.1f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		if (clone == null)
		{
			clone = new Player();
		}
		clone.CopyVisuals(Owner);
		clone.skinColor = Color.Black;
		clone.shirtColor = Color.Black;
		clone.underShirtColor = Color.Black;
		clone.pantsColor = Color.Black;
		clone.shoeColor = Color.Black;
		clone.hairColor = Color.Black;
		clone.eyeColor = Color.Red;
		for (int i = 0; i < clone.dye.Length; i++)
		{
			if (clone.dye[i].type != 2871)
			{
				clone.dye[i].SetDefaults(2871);
			}
		}
		clone.ResetEffects();
		clone.ResetVisibleAccessories();
		clone.DisplayDollUpdate();
		clone.UpdateSocialShadow();
		clone.UpdateDyes();
		clone.PlayerFrame();
		if (Owner.ItemAnimationActive && Owner.altFunctionUse != 2)
		{
			clone.bodyFrame = Owner.bodyFrame;
		}
		else
		{
			clone.bodyFrame.Y = 0;
		}
		clone.legFrame.Y = 0;
		clone.direction = Math.Sign(base.Projectile.DirectionTo(Main.MouseWorld).X);
		Main.PlayerRenderer.DrawPlayer(Main.Camera, clone, base.Projectile.position, 0f, clone.fullRotationOrigin);
		if (Owner.ItemAnimationActive && Owner.altFunctionUse != 2)
		{
			Texture2D Sword = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/TheDarkMaster", (AssetRequestMode)2).Value;
			Vector2 distToPlayer = base.Projectile.position - Owner.position;
			Main.EntitySpriteDraw(Sword, Owner.HandPosition.Value + distToPlayer - Main.screenPosition, null, lightColor, (Owner.direction == clone.direction) ? Owner.itemRotation : (0f - Owner.itemRotation), new Vector2((float)((clone.direction != 1) ? Sword.Width : 0), (float)Sword.Height), 1f, (SpriteEffects)(clone.direction != 1));
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
