const getColorStatus = (color) => {
    const col = `${color}`;
    return '#' + col.substring(1)
};

export default getColorStatus