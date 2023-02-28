﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAdhocToolchain.Core.Instructions
{
    public abstract class InstructionBase
    {
        public abstract AdhocInstructionType InstructionType { get; }
        public abstract string InstructionName { get; }

        public uint LineNumber { get; set; }

        public uint InstructionOffset { get; set; }

        public abstract void Serialize(AdhocStream stream);
        public abstract void Deserialize(AdhocStream stream);
        public abstract string Disassemble(bool asCompareMode = false);

        public virtual bool IsFunctionOrMethod()
        {
            return InstructionType == AdhocInstructionType.METHOD_DEFINE || InstructionType == AdhocInstructionType.FUNCTION_DEFINE
                    || InstructionType == AdhocInstructionType.METHOD_CONST || InstructionType == AdhocInstructionType.FUNCTION_CONST;
        }

        public static InstructionBase GetByType(AdhocInstructionType type)
        {
            return type switch
            {
                AdhocInstructionType.MODULE_DEFINE => new InsModuleDefine(),
                AdhocInstructionType.FUNCTION_CONST => new InsFunctionConst(),
                AdhocInstructionType.METHOD_DEFINE => new InsMethodDefine(),
                AdhocInstructionType.FUNCTION_DEFINE => new InsFunctionDefine(),
                AdhocInstructionType.METHOD_CONST => new InsMethodConst(),
                AdhocInstructionType.VARIABLE_EVAL => new InsVariableEvaluation(),
                AdhocInstructionType.CALL => new InsCall(),
                AdhocInstructionType.CALL_OLD => new InsCallOld(),
                AdhocInstructionType.JUMP_IF_FALSE => new InsJumpIfFalse(),
                AdhocInstructionType.FLOAT_CONST => new InsFloatConst(),
                AdhocInstructionType.ATTRIBUTE_PUSH => new InsAttributePush(),
                AdhocInstructionType.ASSIGN_POP => new InsAssignPop(),
                AdhocInstructionType.LEAVE => new InsLeaveScope(),
                AdhocInstructionType.VOID_CONST => new InsVoidConst(),
                AdhocInstructionType.SET_STATE => new InsSetState(),
                AdhocInstructionType.SET_STATE_OLD => new InsSetStateOld(),
                AdhocInstructionType.NIL_CONST => new InsNilConst(),
                AdhocInstructionType.ATTRIBUTE_DEFINE => new InsAttributeDefine(),
                AdhocInstructionType.BOOL_CONST => new InsBoolConst(),
                AdhocInstructionType.SOURCE_FILE => new InsSourceFile(),
                AdhocInstructionType.IMPORT => new InsImport(),
                AdhocInstructionType.STRING_CONST => new InsStringConst(),
                AdhocInstructionType.POP => new InsPop(),
                AdhocInstructionType.POP_OLD => new InsPopOld(),
                AdhocInstructionType.CLASS_DEFINE => new InsClassDefine(),
                AdhocInstructionType.ATTRIBUTE_EVAL => new InsAttributeEvaluation(),
                AdhocInstructionType.INT_CONST => new InsIntConst(),
                AdhocInstructionType.STATIC_DEFINE => new InsStaticDefine(),
                AdhocInstructionType.VARIABLE_PUSH => new InsVariablePush(),
                AdhocInstructionType.BINARY_OPERATOR => new InsBinaryOperator(),
                AdhocInstructionType.JUMP => new InsJump(),
                AdhocInstructionType.ELEMENT_EVAL => new InsElementEval(),
                AdhocInstructionType.STRING_PUSH => new InsStringPush(),
                AdhocInstructionType.JUMP_IF_TRUE => new InsJumpIfTrue(),
                AdhocInstructionType.EVAL => new InsEval(),
                AdhocInstructionType.BINARY_ASSIGN_OPERATOR => new InsBinaryAssignOperator(),
                AdhocInstructionType.LOGICAL_OR_OLD => new InsLogicalOrOld(),
                AdhocInstructionType.LOGICAL_OR => new InsLogicalOr(),
                AdhocInstructionType.LIST_ASSIGN => new InsListAssign(),
                AdhocInstructionType.LIST_ASSIGN_OLD => new InsListAssignOld(),
                AdhocInstructionType.ELEMENT_PUSH => new InsElementPush(),
                AdhocInstructionType.MAP_CONST => new InsMapConst(),
                AdhocInstructionType.MAP_CONST_OLD => new InsMapConstOld(),
                AdhocInstructionType.MAP_INSERT => new InsMapInsert(),
                AdhocInstructionType.UNARY_OPERATOR => new InsUnaryOperator(),
                AdhocInstructionType.LOGICAL_AND_OLD => new InsLogicalAndOld(),
                AdhocInstructionType.LOGICAL_AND => new InsLogicalAnd(),
                AdhocInstructionType.ARRAY_CONST => new InsArrayConst(),
                AdhocInstructionType.ARRAY_CONST_OLD => new InsArrayConstOld(),
                AdhocInstructionType.ARRAY_PUSH => new InsArrayPush(),
                AdhocInstructionType.UNARY_ASSIGN_OPERATOR => new InsUnaryAssignOperator(),
                AdhocInstructionType.SYMBOL_CONST => new InsSymbolConst(),
                AdhocInstructionType.OBJECT_SELECTOR => new InsObjectSelector(),
                AdhocInstructionType.LONG_CONST => new InsLongConst(),
                AdhocInstructionType.UNDEF => new InsUndef(),
                AdhocInstructionType.TRY_CATCH => new InsTryCatch(),
                AdhocInstructionType.THROW => new InsThrow(),
                AdhocInstructionType.ASSIGN => new InsAssign(),
                AdhocInstructionType.ASSIGN_OLD => new InsAssignOld(),
                AdhocInstructionType.U_INT_CONST => new InsUIntConst(),
                AdhocInstructionType.REQUIRE => new InsRequire(),
                AdhocInstructionType.U_LONG_CONST => new InsULongConst(),
                AdhocInstructionType.PRINT => new InsPrint(),
                AdhocInstructionType.MODULE_CONSTRUCTOR => new InsModuleConstructor(),
                AdhocInstructionType.VA_CALL => new InsVaCall(),
                AdhocInstructionType.NOP => new InsNop(),
                AdhocInstructionType.DOUBLE_CONST => new InsDoubleConst(),
                AdhocInstructionType.DELEGATE_DEFINE => new InsDelegateDefine(),
                AdhocInstructionType.JUMP_IF_NOT_NIL => new InsJumpIfNotNil(),
                AdhocInstructionType.LOGICAL_OPTIONAL => new InsOptional(),
                _ => throw new Exception($"Encountered unimplemented {type} instruction."),
            };
        }
    }

    public enum AdhocInstructionType : byte
    {
        /// <summary>
        /// Also known as ARRAY_PUSH (not the new one)
        /// </summary>
        ARRAY_CONST_OLD,
        ASSIGN_OLD,
        ATTRIBUTE_DEFINE,
        ATTRIBUTE_PUSH,
        BINARY_ASSIGN_OPERATOR,
        BINARY_OPERATOR,
        CALL,
        CLASS_DEFINE,
        EVAL,
        FLOAT_CONST,
        FUNCTION_DEFINE,
        IMPORT,
        INT_CONST,
        JUMP,
        JUMP_IF_TRUE, // Also known as JUMP_NOT_ZERO
        JUMP_IF_FALSE, // Also known as JUMP_ZERO
        LIST_ASSIGN_OLD,
        LOCAL_DEFINE,
        LOGICAL_AND_OLD,
        LOGICAL_OR_OLD,
        METHOD_DEFINE,
        MODULE_DEFINE,
        NIL_CONST,
        NOP,
        POP_OLD,
        PRINT,
        REQUIRE,
        SET_STATE_OLD,
        STATIC_DEFINE,
        STRING_CONST,
        STRING_PUSH,
        THROW,
        TRY_CATCH,
        UNARY_ASSIGN_OPERATOR,
        UNARY_OPERATOR,
        UNDEF,
        VARIABLE_PUSH,
        ATTRIBUTE_EVAL,
        VARIABLE_EVAL,
        SOURCE_FILE,

        // GTHD Release (V10)
        FUNCTION_CONST,
        METHOD_CONST,
        MAP_CONST_OLD,
        LONG_CONST,
        ASSIGN,
        LIST_ASSIGN,
        CALL_OLD,

        // GT5P JP Demo (V10)
        OBJECT_SELECTOR, // Also known as SELF_SELECTOR earlier than GT5P Demo
        SYMBOL_CONST,
        LEAVE, // Also known as CODE_CONST earlier than GT5P Demo

        // V11
        ARRAY_CONST, 
        ARRAY_PUSH,
        MAP_CONST,
        MAP_INSERT,
        POP,
        SET_STATE,
        VOID_CONST,
        ASSIGN_POP,

        // GT5 Spec 3 (V12)
        U_INT_CONST,
        U_LONG_CONST,
        DOUBLE_CONST,

        // GT5 TT Challenge (V12)
        ELEMENT_PUSH,
        ELEMENT_EVAL,
        LOGICAL_AND,
        LOGICAL_OR,
        BOOL_CONST,
        MODULE_CONSTRUCTOR,

        // GT6 (V12)
        VA_CALL,
        CODE_EVAL,

        // GT Sport (V12)
        DELEGATE_DEFINE,
        JUMP_IF_NOT_NIL,
        LOGICAL_OPTIONAL,
    }

    enum AdhocVersion
    {
        GT5P, // V11 -> V12, adds 3 instructions
        GT5TT, // Adds 6 instructions
        GT6, // Adds 2 instructions
        GTSport, // Adds 3 instructions
    }
}
