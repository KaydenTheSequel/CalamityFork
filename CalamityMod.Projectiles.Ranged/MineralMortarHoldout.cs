using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MineralMortarHoldout : BaseGunHoldoutProjectile
{
	public override int AssociatedItemID => ModContent.ItemType<MineralMortar>();

	public override float MaxOffsetLengthFromArm => 15f;

	public override float OffsetXUpwards => -10f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 5f;

	public ref float Time => ref base.Projectile.ai[0];

	public override void HoldoutAI()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mouse = base.Owner.Calamity().mouseWorld;
		Time++;
		if (!(Time >= (float)base.Owner.itemTimeMax) || Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		base.Owner.PickAmmo(base.Owner.HeldItem, out var _, out var speed, out var damage, out var knockback, out var usedItemAmmoId);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * speed * 1.5f, ModContent.ProjectileType<MineralMortarProjectile>(), damage, knockback, base.Projectile.owner, usedItemAmmoId);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 20; i++)
			{
				Vector2 randomDirectionToMouse = base.Owner.SafeDirectionTo(mouse).RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(8f, 10f);
				Vector2 gunTipPosition = GunTipPosition;
				Vector2? velocity = randomDirectionToMouse;
				float scale = Main.rand.NextFloat(1.5f, 2.5f);
				Dust dust = Dust.NewDustPerfect(gunTipPosition, 6, velocity, 0, default(Color), scale);
				dust.noGravity = true;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			for (int j = 0; j < 8; j++)
			{
				Vector2 randomDirectionToMouse2 = base.Owner.SafeDirectionTo(mouse).RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(8f, 10f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(GunTipPosition, randomDirectionToMouse2, new Color(255, 100, 0), Color.Transparent, Main.rand.NextFloat(0.3f, 1f), Main.rand.NextFloat(200f, 400f)));
			}
			for (int k = 0; k < 6; k++)
			{
				Vector2 randomDirectionToMouse3 = base.Owner.SafeDirectionTo(mouse).RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(2f, 6f);
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(GunTipPosition, randomDirectionToMouse3, Color.Lerp(Color.White, Color.LightGray, Main.rand.NextFloat()), Main.rand.NextFloat(0.6f, 0.8f), Main.rand.Next(30, 46)));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ScorchedEarthShot", 3);
			style.Volume = 0.2f;
			style.Pitch = 1.2f;
			style.PitchVariance = 1.1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		base.OffsetLengthFromArm = 0f;
		Time = 0f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		float rotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 origin = value.Size() * 0.5f;
		SpriteEffects effects = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		float shake = Utils.Remap(Time, (float)base.Owner.itemTimeMax * 0.33f, base.Owner.itemTimeMax, 0f, 3f);
		position += Main.rand.NextVector2Circular(shake, shake);
		Main.EntitySpriteDraw(value, position, null, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale * base.Owner.gravDir, effects);
		return false;
	}
}
