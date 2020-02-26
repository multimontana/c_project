
let lat = ["A","B","V","G","D","Ye","E","Yo","Zh","Z","I","J","K","L","M","N","O","P","R","S","T","U","F","H","C","Ch","Sh","Sh","","Yi","","E","Yu","Ya","a","b","v","g","d","e","yo","zh","z","i","j","k","l","m","n","o","p","r","s","t","u","f","h","c","ch","sh","shh","y","yi","","e","yu","ya","_",""];
let rus = ["А","Б","В","Г","Д","Е","Е","Ё","Ж","З","И","Й","К","Л","М","Н","О","П","Р","С","Т","У","Ф","Х","Ц","Ч","Ш","Щ","Ъ","Ы","Ь","Э","Ю","Я","а","б","в","г","д","е","ё","ж","з","и","й","к","л","м","н","о","п","р","с","т","у","ф","х","ц","ч","ш","щ","ъ","ы","ь","э","ю","я"," ","."];
const en_to_rus = (str) => {
   let result = "";
   let checkNext = true;
   for (let i = 0; i < str.length; i++){
      if (checkNext){
         let withNext = str[i] + str[i+1];
         let index = lat.indexOf(withNext);
         if (index === -1){
            index = lat.indexOf(str[i]);
            if (index === -1) {
               result += str[i];
            }else{
               result += rus[index];
            }
         }else{
            checkNext = false;
            result += rus[index];
         }
      }else{
         checkNext = true;
      }
   }
   return result;
}

export default en_to_rus