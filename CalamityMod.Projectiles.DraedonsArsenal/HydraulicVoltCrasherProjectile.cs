using System;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class HydraulicVoltCrasherProjectile : ModProjectile
{
	private int chargeCooldown;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<HydraulicVoltCrasher>();

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 56;
		base.Projectile.height = 56;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft = 60;
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 4f == 3f)
		{
			base.Projectile.frame++;
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.frame = 0;
			}
		}
		if (chargeCooldown > 0)
		{
			chargeCooldown = 0;
		}
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.Item22, base.Projectile.Center);
			base.Projectile.soundDelay = 30;
		}
		Vector2 center = Owner.RotatedRelativePoint(Owner.MountedCenter);
		if (Main.myPlayer == Owner.whoAmI)
		{
			Item heldItem = Owner.HeldItem;
			if (heldItem.type < ItemID.Count)
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.damage = Owner.GetWeaponDamage(heldItem);
			heldItem.Calamity();
			if (Owner.channel || Owner.Calamity().mouseRight)
			{
				float speed = Owner.inventory[Owner.selectedItem].shootSpeed * base.Projectile.scale;
				Vector2 toPointTo = Main.MouseWorld;
				if (Owner.gravDir == -1f)
				{
					toPointTo.Y = (float)Main.screenHeight - toPointTo.Y;
				}
				toPointTo = Vector2.Normalize(toPointTo - center) * speed;
				if (toPointTo != base.Projectile.velocity)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = toPointTo;
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		Owner.ChangeDir((base.Projectile.velocity.X > 0f).ToDirectionInt());
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemAnimation = 2;
		Owner.itemTime = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)Owner.direction).ToRotation();
		base.Projectile.direction = (base.Projectile.spriteDirection = Owner.direction);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)(base.Projectile.direction == -1).ToInt() * (float)Math.PI;
		if (Main.rand.NextBool(5))
		{
			Vector2 spawnPosition = base.Projectile.velocity;
			((Vector2)(ref spawnPosition)).Normalize();
			spawnPosition *= base.Projectile.Size;
			spawnPosition += base.Projectile.Center;
			Dust dust = Dust.NewDustPerfect(spawnPosition, 226);
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(2f, 3.6f);
			dust.velocity += Owner.velocity * 0.4f;
		}
		base.Projectile.position = center - base.Projectile.Size * 0.5f;
		Projectile projectile = base.Projectile;
		projectile.position -= base.Projectile.velocity.ToRotation().ToRotationVector2() * 8f;
		base.Projectile.velocity.X *= Main.rand.NextFloat(0.97f, 1.03f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in CommonCalamitySounds.PlasmaBoltSound, target.Center);
		if (chargeCooldown > 0)
		{
			return;
		}
		chargeCooldown = 60;
		TryToSuperchargeNPC(target);
		int extraZaps = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != target.whoAmI && n.CanBeChasedBy(base.Projectile) && n.Distance(target.Center) < 240f && extraZaps < 3 && TryToSuperchargeNPC(n))
			{
				for (float increment = 0f; increment <= 1f; increment += 0.05f)
				{
					Dust dust = Dust.NewDustPerfect(Vector2.Lerp(target.Center, n.Center, increment), 226);
					dust.velocity = Vector2.Zero;
					dust.scale = 1.6f;
					dust.noGravity = true;
				}
				extraZaps++;
			}
		}
	}

	public bool TryToSuperchargeNPC(NPC npc)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		int attackType = ModContent.ProjectileType<VoltageStream>();
		if (Owner.ownedProjectileCounts[attackType] > 3)
		{
			return false;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == attackType && p.ai[1] == (float)p.whoAmI)
			{
				return false;
			}
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), npc.Center, Vector2.Zero, attackType, base.Projectile.damage, 0f, base.Projectile.owner, 0f, npc.whoAmI);
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), lightColor, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}
}
