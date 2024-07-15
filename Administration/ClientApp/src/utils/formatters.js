export function formatDate(date) {
  const d = new Date(date);
  const day = String(d.getDate()).padStart(2, '0');
  const month = String(d.getMonth() + 1).padStart(2, '0'); // Месяцы от 0 до 11
  const year = d.getFullYear();

  return `${day}.${month}.${year}`;
}

export function isNullOrEmpty(str) {
  return str === null || str === undefined || str.trim() === '';
}
