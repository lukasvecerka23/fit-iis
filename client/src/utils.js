export const KpiFunction = {
    Less: 0,
    LessOrEqual: 1,
    Equal: 2,
    GreaterOrEqual: 3,
    Greater: 4,
    NotEqual: 5
};

export function mapKpiFunctionToString(value) {
    const mapping = {
        [KpiFunction.Less]: "<",
        [KpiFunction.LessOrEqual]: "<=",
        [KpiFunction.Equal]: "=",
        [KpiFunction.GreaterOrEqual]: ">=",
        [KpiFunction.Greater]: ">",
        [KpiFunction.NotEqual]: "≠"
    };
    return mapping[value] || "-";
}