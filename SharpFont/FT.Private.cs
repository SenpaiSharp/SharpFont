﻿#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion

using System;
using System.Runtime.InteropServices;

using SharpFont.Internal;
using SharpFont.PostScript;
using SharpFont.TrueType;

namespace SharpFont
{
	public static partial class FT
	{
		/// <summary>
		/// Defines the location of the FreeType DLL. Update SharpFont.dll.config if you change this!
		/// </summary>
		private const string freetypeDll = "freetype.dll";

		/// <summary>
		/// Defines the calling convention for P/Invoking the native freetype methods.
		/// </summary>
		private const CallingConvention callConvention = CallingConvention.Cdecl;

		#region Core API

		#region FreeType Version

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Library_Version(IntPtr library, out int amajor, out int aminor, out int apatch);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern bool FT_Face_CheckTrueTypePatents(IntPtr face);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern bool FT_Face_SetUnpatentedHinting(IntPtr face, bool value);

		#endregion

		#region Base Interface
		
		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Init_FreeType(out IntPtr alibrary);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Done_FreeType(IntPtr library);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_New_Face(IntPtr library, [MarshalAs(UnmanagedType.LPStr)] string filepathname, int face_index, out IntPtr aface);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_New_Memory_Face(IntPtr library, IntPtr file_base, int file_size, int face_index, out IntPtr aface);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Open_Face(IntPtr library, IntPtr args, int face_index, out IntPtr aface);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Attach_File(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] string filepathname);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Attach_Stream(IntPtr face, IntPtr parameters);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Reference_Face(IntPtr face);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Done_Face(IntPtr face);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Select_Size(IntPtr face, int strike_index);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Request_Size(IntPtr face, IntPtr req);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_Char_Size(IntPtr face, int char_width, int char_height, uint horz_resolution, uint vert_resolution);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_Pixel_Sizes(IntPtr face, uint pixel_width, uint pixel_height);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Load_Glyph(IntPtr face, uint glyph_index, int load_flags);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Load_Char(IntPtr face, uint char_code, int load_flags);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Set_Transform(IntPtr face, ref FTMatrix matrix, ref FTVector delta);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Render_Glyph(IntPtr slot, RenderMode render_mode);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Kerning(IntPtr face, uint left_glyph, uint right_glyph, KerningMode kern_mode, out FTVector akerning);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Track_Kerning(IntPtr face, int point_size, int degree, out int akerning);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Glyph_Name(IntPtr face, uint glyph_index, IntPtr buffer, uint buffer_max);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Get_Postscript_Name(IntPtr face);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Select_Charmap(IntPtr face, Encoding encoding);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_Charmap(IntPtr face, IntPtr charmap);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Get_Charmap_Index(IntPtr charmap);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern uint FT_Get_Char_Index(IntPtr face, uint charcode);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern uint FT_Get_First_Char(IntPtr face, out uint agindex);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern uint FT_Get_Next_Char(IntPtr face, uint char_code, out uint agindex);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern uint FT_Get_Name_Index(IntPtr face, IntPtr glyph_name);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_SubGlyph_Info(IntPtr glyph, uint sub_index, out int p_index, out SubGlyphFlags p_flags, out int p_arg1, out int p_arg2, out FTMatrix p_transform);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern EmbeddingTypes FT_Get_FSType_Flags(IntPtr face);

		#endregion

		#region Glyph Variants

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern uint FT_Face_GetCharVariantIndex(IntPtr face, uint charcode, uint variantSelector);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Face_GetCharVariantIsDefault(IntPtr face, uint charcode, uint variantSelector);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Face_GetVariantSelectors(IntPtr face);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Face_GetVariantsOfChar(IntPtr face, uint charcode);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Face_GetCharsOfVariant(IntPtr face, uint variantSelector);

		#endregion

		#region Glyph Management

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Glyph(IntPtr slot, out IntPtr aglyph);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Glyph_Copy(IntPtr source, out IntPtr target);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Glyph_Transform(IntPtr glyph, ref FTMatrix matrix, ref FTVector delta);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Glyph_Get_CBox(IntPtr glyph, GlyphBBoxMode bbox_mode, out IntPtr acbox);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Glyph_To_Bitmap(ref IntPtr the_glyph, RenderMode render_mode, ref FTVector origin, bool destroy);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Done_Glyph(IntPtr glyph);

		#endregion

		#region Mac Specific Interface

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_New_Face_From_FOND(IntPtr library, IntPtr fond, int face_index, out IntPtr aface);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_GetFile_From_Mac_Name([MarshalAs(UnmanagedType.LPStr)] string fontName, out IntPtr pathSpec, out int face_index);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_GetFile_From_Mac_ATS_Name([MarshalAs(UnmanagedType.LPStr)] string fontName, out IntPtr pathSpec, out int face_index);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_GetFilePath_From_Mac_ATS_Name([MarshalAs(UnmanagedType.LPStr)] string fontName, IntPtr path, int maxPathSize, out int face_index);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_New_Face_From_FSSpec(IntPtr library, IntPtr spec, int face_index, out IntPtr aface);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_New_Face_From_FSRef(IntPtr library, IntPtr @ref, int face_index, out IntPtr aface);

		#endregion

		#region Size Management

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_New_Size(IntPtr face, out IntPtr size);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Done_Size(IntPtr size);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Activate_Size(IntPtr size);

		#endregion

		#endregion

		#region Format-Specific API

		#region Multiple Masters

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Multi_Master(IntPtr face, out IntPtr amaster);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_MM_Var(IntPtr face, out IntPtr amaster);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_MM_Design_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_Var_Design_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_MM_Blend_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_Var_Blend_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		#endregion

		#region TrueType Tables

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Get_Sfnt_Table(IntPtr face, SfntTag tag);

		//TODO find FT_TRUETYPE_TAGS_H and create an enum for "tag"
		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Load_Sfnt_Table(IntPtr face, uint tag, int offset, IntPtr buffer, ref uint length);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal unsafe static extern Error FT_Sfnt_Table_Info(IntPtr face, uint table_index, SfntTag *tag, out uint length);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern uint FT_Get_CMap_Language_ID(IntPtr charmap);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Get_CMap_Format(IntPtr charmap);

		#endregion

		#region Type 1 Tables

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern bool FT_Has_PS_Glyph_Names(IntPtr face);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_PS_Font_Info(IntPtr face, out IntPtr afont_info);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_PS_Font_Private(IntPtr face, out IntPtr afont_private);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Get_PS_Font_Value(IntPtr face, DictionaryKeys key, uint idx, ref IntPtr value, int value_len);

		#endregion

		#region SFNT Names

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern uint FT_Get_Sfnt_Name_Count(IntPtr face);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Sfnt_Name(IntPtr face, uint idx, out IntPtr aname);

		#endregion

		#region BDF and PCF Files

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_BDF_Charset_ID(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] out string acharset_encoding, [MarshalAs(UnmanagedType.LPStr)] out string acharset_registry);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_BDF_Property(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] string prop_name, out IntPtr aproperty);

		#endregion

		#region CID Fonts

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_CID_Registry_Ordering_Supplement(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] out string registry, [MarshalAs(UnmanagedType.LPStr)] out string ordering, out int aproperty);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_CID_Is_Internally_CID_Keyed(IntPtr face, out byte is_cid);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_CID_From_Glyph_Index(IntPtr face, uint glyph_index, out uint cid);

		#endregion

		#region PFR Fonts

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_PFR_Metrics(IntPtr face, out uint aoutline_resolution, out uint ametrics_resolution, out int ametrics_x_scale, out int ametrics_y_scale);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_PFR_Kerning(IntPtr face, uint left, uint right, out FTVector avector);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_PFR_Advance(IntPtr face, uint gindex, out int aadvance);

		#endregion

		#region Window FNT Files

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_WinFNT_Header(IntPtr face, out IntPtr aheader);

		#endregion

		#region Font Formats

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Get_X11_Font_Format(IntPtr face);

		#endregion

		#region Gasp Table

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Gasp FT_Get_Gasp(IntPtr face, uint ppem);

		#endregion

		#endregion

		#region Support API

		#region Computations

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_MulDiv(int a, int b, int c);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_MulFix(int a, int b);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_DivFix(int a, int b);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_RoundFix(int a);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_CeilFix(int a);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_FloorFix(int a);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Vector_Transform(ref FTVector vec, ref FTMatrix matrix);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Matrix_Multiply(ref FTMatrix a, ref FTMatrix b);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Matrix_Invert(ref FTMatrix matrix);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Sin(int angle);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Cos(int angle);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Tan(int angle);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Atan2(int x, int y);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Angle_Diff(int angle1, int angle2);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Vector_Unit(out FTVector vec, int angle);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Vector_Rotate(ref FTVector vec, int angle);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern int FT_Vector_Length(ref FTVector vec);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Vector_Polarize(ref FTVector vec, out int length, out int angle);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Vector_From_Polar(out FTVector vec, int length, int angle);

		#endregion

		#region List Processing

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_List_Find(IntPtr list, IntPtr data);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_List_Add(IntPtr list, IntPtr node);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_List_Insert(IntPtr list, IntPtr node);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_List_Remove(IntPtr list, IntPtr node);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_List_Up(IntPtr list, IntPtr node);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_List_Iterate(IntPtr list, ListIterator iterator, IntPtr user);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_List_Finalize(IntPtr list, ListDestructor destroy, IntPtr memory, IntPtr user);

		#endregion

		#region Outline Processing

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_New(IntPtr library, uint numPoints, int numContours, out IntPtr anoutline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_New_Internal(IntPtr memory, uint numPoints, int numContours, out IntPtr anoutline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Done(IntPtr library, IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Done_Internal(IntPtr memory, IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Copy(IntPtr source, ref IntPtr target);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Outline_Translate(IntPtr outline, int xOffset, int yOffset);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Outline_Transform(IntPtr outline, ref FTMatrix matrix);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Embolden(IntPtr outline, int strength);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_EmboldenXY(IntPtr outline, int xstrength, int ystrength);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Outline_Reverse(IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Check(IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Get_BBox(IntPtr outline, out IntPtr abbox);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Decompose(IntPtr outline, IntPtr func_interface, IntPtr user);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Outline_Get_CBox(IntPtr outline, out IntPtr acbox);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Get_Bitmap(IntPtr library, IntPtr outline, IntPtr abitmap);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Outline_Render(IntPtr library, IntPtr outline, IntPtr @params);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Orientation FT_Outline_Get_Orientation(IntPtr outline);

		#endregion

		#region Quick retrieval of advance values

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Advance(IntPtr face, uint gIndex, LoadFlags load_flags, out int padvance);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Get_Advances(IntPtr face, uint start, uint count, LoadFlags load_flags, out IntPtr padvance);

		#endregion

		#region Bitmap Handling

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Bitmap_New(out IntPtr abitmap);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Bitmap_Copy(IntPtr library, IntPtr source, out IntPtr target);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Bitmap_Embolden(IntPtr library, IntPtr bitmap, int xStrength, int yStrength);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Bitmap_Convert(IntPtr library, IntPtr source, out IntPtr target, int alignment);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_GlyphSlot_Own_Bitmap(IntPtr slot);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Bitmap_Done(IntPtr library, IntPtr bitmap);

		#endregion

		#region Glyph Stroker

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern StrokerBorder FT_Outline_GetInsideBorder(IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern StrokerBorder FT_Outline_GetOutsideBorder(IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_New(IntPtr library, out IntPtr astroker);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Stroker_Set(IntPtr stroker, int radius, StrokerLineCap line_cap, StrokerLineJoin line_join, int miter_limit);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Stroker_Rewind(IntPtr stroker);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_ParseOutline(IntPtr stroker, IntPtr outline, bool opened);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_BeginSubPath(IntPtr stroker, ref FTVector to, bool open);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_EndSubPath(IntPtr stroker);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_LineTo(IntPtr stroker, ref FTVector to);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_ConicTo(IntPtr stroker, ref FTVector control, ref FTVector to);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_CubicTo(IntPtr stroker, ref FTVector control1, ref FTVector control2, ref FTVector to);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_GetBorderCounts(IntPtr stroker, StrokerBorder border, out uint anum_points, out uint anum_contours);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Stroker_ExportBorder(IntPtr stroker, StrokerBorder border, IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stroker_GetCounts(IntPtr stroker, out uint anum_points, out uint anum_contours);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Stroker_Export(IntPtr stroker, IntPtr outline);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Stroker_Done(IntPtr stroker);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Glyph_Stroke(ref IntPtr pglyph, IntPtr stoker, bool destroy);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Glyph_StrokeBorder(ref IntPtr pglyph, IntPtr stoker, bool inside, bool destroy);

		#endregion

		#region Module Management

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Add_Module(IntPtr library, IntPtr clazz);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Get_Module(IntPtr library, [MarshalAs(UnmanagedType.LPStr)] string module_name);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Remove_Module(IntPtr library, IntPtr module);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Reference_Library(IntPtr library);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_New_Library(IntPtr memory, out IntPtr alibrary);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Done_Library(IntPtr library);

		//TODO figure out the method signature for debug_hook. (FT_DebugHook_Func)
		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Set_Debug_Hook(IntPtr library, uint hook_index, IntPtr debug_hook);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_Add_Default_Modules(IntPtr library);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern IntPtr FT_Get_Renderer(IntPtr library, GlyphFormat format);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Set_Renderer(IntPtr library, IntPtr renderer, uint num_params, IntPtr parameters);

		#endregion

		#region GZIP Streams

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stream_OpenGzip(IntPtr stream, IntPtr source);

		#endregion

		#region LZW Streams

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stream_OpenLZW(IntPtr stream, IntPtr source);

		#endregion

		#region BZIP2 Streams

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Stream_OpenBzip2(IntPtr stream, IntPtr source);

		#endregion

		#region LCD Filtering

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Library_SetLcdFilter(IntPtr library, LcdFilter filter);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_Library_SetLcdFilterWeights(IntPtr library, byte[] weights);

		#endregion

		#endregion

		#region Miscellaneous

		#region OpenType Validation

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_OpenType_Validate(IntPtr face, OpenTypeValidationFlags validation_flags, out IntPtr BASE_table, out IntPtr GDEF_table, out IntPtr GPOS_table, out IntPtr GSUB_table, out IntPtr JSFT_table);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern void FT_OpenType_Free(IntPtr face, IntPtr table);

		#endregion

		#region The TrueType Engine

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern EngineType FT_Get_TrueType_Engine_Type(IntPtr library);

		#endregion

		#region TrueTypeGX/AAT Validation

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_TrueTypeGX_Validate(IntPtr face, TrueTypeValidationFlags validation_flags, byte[][] tables, uint tableLength);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_TrueTypeGX_Free(IntPtr face, IntPtr table);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_ClassicKern_Validate(IntPtr face, ClassicKernValidationFlags validation_flags, out IntPtr ckern_table);

		[DllImport(freetypeDll, CallingConvention = callConvention)]
		internal static extern Error FT_ClassicKern_Free(IntPtr face, IntPtr table);

		#endregion

		#endregion
	}
}
