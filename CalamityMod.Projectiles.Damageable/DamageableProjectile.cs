using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Damageable;

public abstract class DamageableProjectile : ModProjectile
{
	public int Life;

	public int DamageImmunityFrames;

	public abstract int LifeMax { get; }

	public abstract SoundStyle HitSound { get; }

	public abstract SoundStyle DeathSound { get; }

	public abstract DamageSourceType DamageSources { get; }

	public virtual bool DrawHPBar { get; set; } = true;

	public virtual bool DrawHPBarAtFullHealth { get; set; }

	public virtual int MaxDamageImmunityFrames { get; set; } = 10;

	public virtual List<int> NPCsToIgnore { get; set; } = new List<int>();

	public virtual List<int> ProjectilesToIgnore { get; set; } = new List<int>();

	public virtual void SafeSetDefaults()
	{
	}

	public sealed override void SetDefaults()
	{
		SafeSetDefaults();
		Life = LifeMax;
	}

	public sealed override void PostDraw(Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		DrawHealthBar();
		MouseOverText();
		SafePostDraw(Main.spriteBatch, lightColor);
	}

	public virtual void SafePostDraw(SpriteBatch spriteBatch, Color lightColor)
	{
	}

	public sealed override void AI()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		SafeAI();
		if (DamageImmunityFrames > 0)
		{
			DamageImmunityFrames--;
			return;
		}
		bool wasHit = NPCCollisionCheck();
		if (!wasHit)
		{
			wasHit = ProjectileCollisionCheck();
		}
		if (wasHit && Life > 0)
		{
			SoundEngine.PlaySound(HitSound, base.Projectile.Center);
		}
		else if (Life <= 0)
		{
			SoundEngine.PlaySound(DeathSound, base.Projectile.Center);
			DamageKillEffect();
			base.Projectile.Kill();
		}
	}

	public virtual bool NPCCollisionCheck()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (!n.IsAnEnemy() || !DamageSources.HasFlag(DamageSourceType.HostileNPCs))
			{
				continue;
			}
			Rectangle hitbox = n.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(base.Projectile.Hitbox) && !NPCsToIgnore.Contains(n.type))
			{
				int damage = Main.DamageVar(n.damage);
				int bannerBuffId = Item.NPCtoBanner(n.BannerID());
				if (bannerBuffId > 0 && player.HasNPCBannerBuff(bannerBuffId))
				{
					damage = ((!Main.expertMode) ? ((int)((float)damage * ItemID.Sets.BannerStrength[Item.BannerToItem(bannerBuffId)].NormalDamageReceived)) : ((int)((float)damage * ItemID.Sets.BannerStrength[Item.BannerToItem(bannerBuffId)].ExpertDamageReceived)));
				}
				CombatText.NewText(new Rectangle((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height), CombatText.DamagedFriendly, damage);
				Life -= damage;
				HitEffectNPC(damage, n);
				DamageImmunityFrames = MaxDamageImmunityFrames;
				NetUpdate(force: true);
				return true;
			}
		}
		return false;
	}

	public virtual bool ProjectileCollisionCheck()
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (((p.friendly && DamageSources.HasFlag(DamageSourceType.FriendlyProjectiles)) || (p.hostile && DamageSources.HasFlag(DamageSourceType.HostileProjectiles))) && p.whoAmI != base.Projectile.whoAmI && p.damage > 0 && p.Colliding(p.Hitbox, base.Projectile.Hitbox))
			{
				int damage = Main.DamageVar(p.damage) * 2;
				damage = (Main.expertMode ? ((int)((float)damage * Main.RegisteredGameModes[1].EnemyDamageMultiplier)) : damage);
				CombatText.NewText(new Rectangle((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height), CombatText.DamagedFriendly, damage);
				Life -= damage;
				HitEffectProjectile(damage, p);
				DamageImmunityFrames = MaxDamageImmunityFrames;
				if (base.Projectile.usesIDStaticNPCImmunity)
				{
					DamageImmunityFrames = base.Projectile.idStaticNPCHitCooldown;
				}
				if (base.Projectile.usesLocalNPCImmunity)
				{
					DamageImmunityFrames = base.Projectile.localNPCHitCooldown;
				}
				NetUpdate(force: true);
				return true;
			}
		}
		return false;
	}

	public virtual void DamageKillEffect()
	{
	}

	public virtual void SafeAI()
	{
	}

	public sealed override void SendExtraAI(BinaryWriter writer)
	{
		SafeSendExtraAI(writer);
		writer.Write(Life);
		writer.Write(DamageImmunityFrames);
	}

	public sealed override void ReceiveExtraAI(BinaryReader reader)
	{
		SafeReceiveExtraAI(reader);
		Life = reader.ReadInt32();
		DamageImmunityFrames = reader.ReadInt32();
	}

	public virtual void SafeSendExtraAI(BinaryWriter writer)
	{
	}

	public virtual void SafeReceiveExtraAI(BinaryReader reader)
	{
	}

	public virtual void NetUpdate(bool force = false, Func<bool> forceCondition = null)
	{
		if (forceCondition == null)
		{
			forceCondition = () => Main.netMode != 1;
		}
		if (force)
		{
			if (forceCondition())
			{
				NetMessage.SendData(27, -1, -1, null, base.Projectile.whoAmI);
			}
		}
		else
		{
			base.Projectile.netUpdate = true;
		}
	}

	public virtual void HitEffectProjectile(int damage, Projectile target)
	{
	}

	public virtual void HitEffectNPC(int damage, NPC target)
	{
	}

	public void MouseOverText()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.mouseText)
		{
			Rectangle mouseRectangle = default(Rectangle);
			((Rectangle)(ref mouseRectangle))._002Ector((int)((float)Main.mouseX + Main.screenPosition.X), (int)((float)Main.mouseY + Main.screenPosition.Y), 1, 1);
			if (Main.LocalPlayer.gravDir == -1f)
			{
				mouseRectangle.Y = (int)Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
			}
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(mouseRectangle) && LifeMax > 1)
			{
				string lifeDataText = base.Projectile.Name + ": " + Life + "/" + LifeMax;
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
				Main.instance.MouseTextHackZoom(lifeDataText);
			}
		}
	}

	public void DrawHealthBar()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (Life != LifeMax || DrawHPBarAtFullHealth)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			Main.instance.DrawHealthBar(base.Projectile.Bottom.X, base.Projectile.Bottom.Y, Life, LifeMax, 1f);
		}
	}
}
