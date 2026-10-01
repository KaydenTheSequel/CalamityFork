using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WulfrumHook : ModProjectile, ILocalizedModType, IModType
{
	public enum HookState
	{
		Thrown = 0,
		Retracting = 1,
		Grappling = 3
	}

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public int EquippedHook => Owner.miscEquips[4].type;

	public HookState State
	{
		get
		{
			return (HookState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public ref float Timer => ref base.Projectile.ai[1];

	public float MaxReach => Owner.GetModPlayer<WulfrumPackPlayer>().ActualMaxLength;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 3000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 3;
		base.Projectile.height = 3;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 3;
		base.Projectile.netImportant = true;
		base.Projectile.aiStyle = 7;
	}

	public override bool? CanUseGrapple(Player player)
	{
		if (player.TryGetModPlayer<WulfrumPackPlayer>(out var mPlayer) && mPlayer.hookCooldown > 0)
		{
			return false;
		}
		return base.CanUseGrapple(player);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreAI()
	{
		return false;
	}

	public override void PostAI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color deepSkyBlue = Color.DeepSkyBlue;
		Lighting.AddLight(center, ((Color)(ref deepSkyBlue)).ToVector3());
		Vector2 BetweenOwner = Owner.Center - base.Projectile.Center;
		if (Owner.dead || Owner.stoned || Owner.webbed || Owner.frozen || ((Vector2)(ref BetweenOwner)).Length() > 1500f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.rotation = BetweenOwner.ToRotation() - (float)Math.PI / 2f;
		if (Owner.GetModPlayer<WulfrumPackPlayer>().WulfrumPackEquipped)
		{
			base.Projectile.timeLeft = 2;
		}
		if (State == HookState.Thrown)
		{
			if (MaxReach < ((Vector2)(ref BetweenOwner)).Length())
			{
				State = HookState.Retracting;
			}
			float fallSpeed = base.Projectile.velocity.Y;
			if (Timer > (float)(15 * base.Projectile.extraUpdates))
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += Vector2.UnitY * 0.5f * (1f - Math.Clamp((Timer - 15f) / 35f, 0f, 1f)) / (float)base.Projectile.extraUpdates;
			}
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.98f;
			if ((double)base.Projectile.velocity.Y + 0.001 > 0.0)
			{
				base.Projectile.velocity.Y = Math.Clamp(base.Projectile.velocity.Y, 0f, Math.Max(18f, fallSpeed));
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 1f)
			{
				State = HookState.Retracting;
			}
			CheckForGrapplableTiles();
		}
		else if (State == HookState.Retracting)
		{
			base.Projectile.velocity = BetweenOwner.SafeNormalize(Vector2.One) * Owner.GetModPlayer<WulfrumPackPlayer>().ActualReturnVelocity;
			Projectile projectile3 = base.Projectile;
			projectile3.Center += Vector2.UnitY * 0.5f;
			if (((Vector2)(ref BetweenOwner)).Length() < 25f)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			if (((Vector2)(ref BetweenOwner)).Length() > Owner.GetModPlayer<WulfrumPackPlayer>().SwingLength + 60f)
			{
				State = HookState.Retracting;
			}
			Point tilePos = base.Projectile.Center.ToTileCoordinates();
			Tile tile = Main.tile[tilePos];
			if (!tile.HasUnactuatedTile || !tile.CanTileBeLatchedOnTo(EquippedHook == 4759) || Owner.IsBlacklistedForGrappling(tilePos))
			{
				State = HookState.Retracting;
			}
			base.Projectile.velocity = Vector2.Zero;
			if (Owner.grapCount < 10)
			{
				Owner.grappling[Owner.grapCount] = base.Projectile.whoAmI;
				Owner.grapCount++;
			}
		}
		Timer++;
	}

	public void CheckForGrapplableTiles()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		Vector2 hitboxStart = base.Projectile.Center - new Vector2(5f);
		Vector2 val = base.Projectile.Center + new Vector2(5f);
		Point topLeftTile = (hitboxStart - new Vector2(16f)).ToTileCoordinates();
		Point bottomRightTile = (val + new Vector2(32f)).ToTileCoordinates();
		Vector2 worldPos = default(Vector2);
		Point tilePos = default(Point);
		for (int x = topLeftTile.X; x < bottomRightTile.X; x++)
		{
			for (int y = topLeftTile.Y; y < bottomRightTile.Y; y++)
			{
				((Vector2)(ref worldPos))._002Ector((float)x * 16f, (float)y * 16f);
				((Point)(ref tilePos))._002Ector(x, y);
				if (hitboxStart.X + 10f > worldPos.X && hitboxStart.X < worldPos.X + 16f && hitboxStart.Y + 10f > worldPos.Y && hitboxStart.Y < worldPos.Y + 16f)
				{
					Tile tile = Main.tile[tilePos];
					if (tile.HasUnactuatedTile && tile.CanTileBeLatchedOnTo(EquippedHook == 4759 && base.Projectile.Distance(Owner.Center) > 96f) && !Owner.IsBlacklistedForGrappling(tilePos))
					{
						OnGrapple(worldPos, x, y);
						break;
					}
				}
			}
			if (State == HookState.Grappling)
			{
				break;
			}
		}
	}

	public void OnGrapple(Vector2 grapplePos, int x, int y)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		WulfrumPackPlayer mp = Owner.GetModPlayer<WulfrumPackPlayer>();
		Owner.ClearGrapplingBlacklist();
		Owner.grappling[0] = -1;
		Owner.grapCount = 0;
		for (int i = 0; i < 1000; i++)
		{
			if (Main.projectile[i].active && Main.projectile[i].owner == Owner.whoAmI && Main.projectile[i].aiStyle == 7 && Main.projectile[i].whoAmI != base.Projectile.whoAmI)
			{
				Main.projectile[i].Kill();
			}
		}
		base.Projectile.velocity = Vector2.Zero;
		State = HookState.Grappling;
		base.Projectile.Center = grapplePos + Vector2.One * 8f;
		WorldGen.KillTile(x, y, fail: true, effectOnly: true);
		SoundEngine.PlaySound(in SoundID.Dig, grapplePos);
		SoundEngine.PlaySound(in WulfrumAcrobaticsPack.GrabSound, grapplePos);
		if (Owner.grapCount < 10)
		{
			Owner.grappling[Owner.grapCount] = base.Projectile.whoAmI;
			Owner.grapCount++;
		}
		Vector2 center;
		if (EquippedHook == 4980 && Owner.whoAmI == Main.myPlayer)
		{
			Player owner = Owner;
			center = Owner.Center - base.Projectile.Center;
			Vector2 spinningpoint = new Vector2((0f - ((Vector2)(ref center)).Length()) * 0.75f, 0f);
			double radians = base.Projectile.DirectionTo(Owner.Center).ToRotation();
			center = default(Vector2);
			owner.DoQueenSlimeHookTeleport(grapplePos + Utils.RotatedBy(spinningpoint, radians, center));
		}
		center = Owner.Center - base.Projectile.Center;
		mp.SwingLength = ((Vector2)(ref center)).Length();
		mp.OldPosition = Owner.Center - Owner.velocity;
		mp.SetSegments(base.Projectile.Center);
		mp.Grapple = base.Projectile.whoAmI;
		Rectangle? tileVisualHitbox = WorldGen.GetTileVisualHitbox(x, y);
		if (tileVisualHitbox.HasValue)
		{
			Projectile projectile = base.Projectile;
			Rectangle value = tileVisualHitbox.Value;
			projectile.Center = ((Rectangle)(ref value)).Center.ToVector2();
		}
		if (Owner.whoAmI == Main.myPlayer && Main.netMode == 1)
		{
			base.Projectile.netUpdate = true;
			NetMessage.SendData(13, -1, -1, null, Owner.whoAmI);
			WulfrumAcrobaticsSync.Send(Owner, mp, base.Projectile);
		}
	}

	public override void OnKill(int timeLeft)
	{
		if (Owner.grappling[0] == base.Projectile.whoAmI)
		{
			Owner.grappling[0] = -1;
			Owner.grapCount--;
		}
	}

	public float PrimWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 1.6f;
	}

	public Color PrimColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Color EndColor = Color.GreenYellow;
		switch (EquippedHook)
		{
		case 2800:
			EndColor = Color.Aquamarine;
			break;
		case 4980:
			EndColor = Color.HotPink;
			break;
		case 4759:
			EndColor = Color.DarkOrange;
			break;
		case 3623:
			EndColor = Color.Silver;
			break;
		}
		return Color.Lerp(Color.DeepSkyBlue, EndColor, (float)Math.Pow(completionRatio, 1.5));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		Vector2[] segmentPositions = (Vector2[])(object)new Vector2[2]
		{
			base.Projectile.Center,
			Owner.Center
		};
		if (State == HookState.Grappling)
		{
			segmentPositions = Owner.GetModPlayer<WulfrumPackPlayer>().Segments.Select(delegate(VerletSimulatedSegment x)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return x.position;
			}).ToArray();
		}
		PrimitiveRenderer.RenderTrail(new List<Vector2>(segmentPositions) { Owner.Center }, new PrimitiveSettings(PrimWidthFunction, PrimColorFunction, null, State == HookState.Grappling), 30);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, texture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool PreDrawExtras()
	{
		return false;
	}
}
